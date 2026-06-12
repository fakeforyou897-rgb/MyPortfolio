using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data.UnitOfWork;
using MyPortfolio.Extensions;
using MyPortfolio.Models.Common;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.Enums;
using MyPortfolio.Models.ViewModels;
using MyPortfolio.Services.Interfaces;

namespace MyPortfolio.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;
        private readonly ITagService _tagService;

        public BlogPostService(IUnitOfWork unitOfWork, IMappingService mappingService, ITagService tagService)
        {
            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
            _tagService = tagService;
        }

        // Query operations
        public async Task<BlogPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.QueryIncluding(
                b => b.Category!,
                b => b.BlogPostTags
            ).FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<BlogPost?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.GetBySlugAsync(slug, cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetPublishedAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.GetPublishedPostsAsync(cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetFeaturedAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.GetFeaturedPostsAsync(count, cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetRecentAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.GetRecentPostsAsync(count, cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.GetPostsByCategoryAsync(categoryId, cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetByTagAsync(Guid tagId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.GetPostsByTagAsync(tagId, cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetByStatusAsync(BlogPostStatus status, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.GetPostsByStatusAsync(status, cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> GetByAuthorAsync(string author, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.FindAsync(b => b.Author == author, cancellationToken);
        }

        // Search and filter
        public async Task<PagedResult<BlogPost>> SearchAsync(BlogPostQueryParameters parameters, CancellationToken cancellationToken = default)
        {
            var query = _unitOfWork.BlogPosts.Query();

            // Apply search
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.ApplySearch(parameters.SearchTerm, b => b.Title, b => b.Content, b => b.Summary);
            }

            // Apply filters
            if (parameters.CategoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == parameters.CategoryId.Value);
            }

            if (parameters.Status.HasValue)
            {
                query = query.Where(b => b.Status == parameters.Status.Value);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Author))
            {
                query = query.Where(b => b.Author == parameters.Author);
            }

            if (parameters.IsFeatured.HasValue)
            {
                query = query.Where(b => b.IsFeatured == parameters.IsFeatured.Value);
            }

            if (parameters.IsPublished.HasValue)
            {
                query = query.Where(b => b.IsPublished == parameters.IsPublished.Value);
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                query = query.ApplySort(parameters.SortBy, parameters.SortDescending);
            }
            else
            {
                query = query.OrderByDescending(b => b.PublishedAt ?? b.CreatedAt);
            }

            // Apply pagination
            return await query.ToPagedResultAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
        }

        public async Task<IEnumerable<BlogPost>> SearchByTermAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.SearchPostsAsync(searchTerm, cancellationToken);
        }

        // Command operations
        public async Task<BlogPost> CreateAsync(BlogPostViewModel model, CancellationToken cancellationToken = default)
        {
            var blogPost = _mappingService.MapToEntity(model);

            // Ensure unique slug
            var slug = blogPost.Slug;
            var counter = 1;
            while (await SlugExistsAsync(slug, null, cancellationToken))
            {
                slug = $"{blogPost.Slug}-{counter}";
                counter++;
            }
            blogPost.Slug = slug;

            await _unitOfWork.BlogPosts.AddAsync(blogPost, cancellationToken);

            // Handle tags if provided
            if (!string.IsNullOrWhiteSpace(model.Tags))
            {
                var tags = await _tagService.ParseTagsAsync(model.Tags, ',', cancellationToken);
                foreach (var tag in tags)
                {
                    blogPost.BlogPostTags.Add(new BlogPostTag { BlogPostId = blogPost.Id, TagId = tag.Id });
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return blogPost;
        }

        public async Task<BlogPost> UpdateAsync(Guid id, BlogPostViewModel model, CancellationToken cancellationToken = default)
        {
            var blogPost = await GetByIdAsync(id, cancellationToken);
            if (blogPost == null)
                throw new KeyNotFoundException($"Blog post with ID {id} not found");

            _mappingService.UpdateEntity(blogPost, model);

            // Ensure unique slug (excluding current post)
            var slug = blogPost.Slug;
            var counter = 1;
            while (await SlugExistsAsync(slug, id, cancellationToken))
            {
                slug = $"{model.Title.ToSlug()}-{counter}";
                counter++;
            }
            blogPost.Slug = slug;

            _unitOfWork.BlogPosts.Update(blogPost);

            // Handle tags if provided
            if (!string.IsNullOrWhiteSpace(model.Tags))
            {
                // Remove existing tags
                blogPost.BlogPostTags.Clear();

                // Add new tags
                var tags = await _tagService.ParseTagsAsync(model.Tags, ',', cancellationToken);
                foreach (var tag in tags)
                {
                    blogPost.BlogPostTags.Add(new BlogPostTag { BlogPostId = blogPost.Id, TagId = tag.Id });
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return blogPost;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var blogPost = await GetByIdAsync(id, cancellationToken);
            if (blogPost == null)
                return false;

            _unitOfWork.BlogPosts.Remove(blogPost);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var blogPost = await GetByIdAsync(id, cancellationToken);
            if (blogPost == null)
                return false;

            _unitOfWork.BlogPosts.SoftDelete(blogPost);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var blogPost = await _unitOfWork.BlogPosts.GetByIdAsync(id, cancellationToken);
            if (blogPost == null)
                return false;

            _unitOfWork.BlogPosts.Restore(blogPost);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        // Business operations
        public async Task<bool> IncrementViewCountAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var blogPost = await GetByIdAsync(id, cancellationToken);
            if (blogPost == null)
                return false;

            blogPost.ViewCount++;
            _unitOfWork.BlogPosts.Update(blogPost);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> ToggleFeaturedAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var blogPost = await GetByIdAsync(id, cancellationToken);
            if (blogPost == null)
                return false;

            blogPost.IsFeatured = !blogPost.IsFeatured;
            _unitOfWork.BlogPosts.Update(blogPost);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> PublishAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var blogPost = await GetByIdAsync(id, cancellationToken);
            if (blogPost == null)
                return false;

            blogPost.Status = BlogPostStatus.Published;
            blogPost.IsPublished = true;
            blogPost.PublishedAt = DateTime.UtcNow;
            _unitOfWork.BlogPosts.Update(blogPost);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> UnpublishAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var blogPost = await GetByIdAsync(id, cancellationToken);
            if (blogPost == null)
                return false;

            blogPost.Status = BlogPostStatus.Draft;
            blogPost.IsPublished = false;
            _unitOfWork.BlogPosts.Update(blogPost);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> UpdateStatusAsync(Guid id, BlogPostStatus status, CancellationToken cancellationToken = default)
        {
            var blogPost = await GetByIdAsync(id, cancellationToken);
            if (blogPost == null)
                return false;

            blogPost.Status = status;
            if (status == BlogPostStatus.Published)
            {
                blogPost.IsPublished = true;
                blogPost.PublishedAt = DateTime.UtcNow;
            }
            _unitOfWork.BlogPosts.Update(blogPost);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> AssignCategoryAsync(Guid postId, Guid? categoryId, CancellationToken cancellationToken = default)
        {
            var blogPost = await GetByIdAsync(postId, cancellationToken);
            if (blogPost == null)
                return false;

            blogPost.CategoryId = categoryId;
            _unitOfWork.BlogPosts.Update(blogPost);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> AssignTagsAsync(Guid postId, List<Guid> tagIds, CancellationToken cancellationToken = default)
        {
            var blogPost = await GetByIdAsync(postId, cancellationToken);
            if (blogPost == null)
                return false;

            // Remove existing tags
            blogPost.BlogPostTags.Clear();

            // Add new tags
            foreach (var tagId in tagIds)
            {
                blogPost.BlogPostTags.Add(new BlogPostTag { BlogPostId = postId, TagId = tagId });
            }

            _unitOfWork.BlogPosts.Update(blogPost);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<int> CalculateReadingTimeAsync(string content, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(content.CalculateReadingTime());
        }

        // Validation
        public async Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.SlugExistsAsync(slug, excludeId, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.BlogPosts.AnyAsync(b => b.Id == id, cancellationToken);
        }
    }
}
