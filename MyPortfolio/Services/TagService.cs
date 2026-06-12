using MyPortfolio.Data.UnitOfWork;
using MyPortfolio.Extensions;
using MyPortfolio.Models.Entities;
using MyPortfolio.Services.Interfaces;

namespace MyPortfolio.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Query operations
        public async Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Tags.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Tags.FirstOrDefaultAsync(t => t.Name == name, cancellationToken);
        }

        public async Task<Tag?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Tags.FirstOrDefaultAsync(t => t.Slug == slug, cancellationToken);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Tags.GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<Tag>> GetPopularAsync(int count, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Tags.GetPopularAsync(count, cancellationToken);
        }

        public async Task<IEnumerable<Tag>> GetTagsWithCountsAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Tags.GetTagsWithCountsAsync(cancellationToken);
        }

        public async Task<IEnumerable<Tag>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Tags.GetByProjectAsync(projectId, cancellationToken);
        }

        public async Task<IEnumerable<Tag>> GetByBlogPostAsync(Guid blogPostId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Tags.GetByBlogPostAsync(blogPostId, cancellationToken);
        }

        // Command operations
        public async Task<Tag> CreateAsync(string name, CancellationToken cancellationToken = default)
        {
            var slug = name.ToSlug();

            // Ensure unique slug
            var counter = 1;
            var originalSlug = slug;
            while (await SlugExistsAsync(slug, null, cancellationToken))
            {
                slug = $"{originalSlug}-{counter}";
                counter++;
            }

            var tag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = name,
                Slug = slug
            };

            await _unitOfWork.Tags.AddAsync(tag, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return tag;
        }

        public async Task<Tag> UpdateAsync(Guid id, string name, CancellationToken cancellationToken = default)
        {
            var tag = await GetByIdAsync(id, cancellationToken);
            if (tag == null)
                throw new KeyNotFoundException($"Tag with ID {id} not found");

            tag.Name = name;
            tag.Slug = name.ToSlug();

            // Ensure unique slug (excluding current tag)
            var slug = tag.Slug;
            var counter = 1;
            var originalSlug = slug;
            while (await SlugExistsAsync(slug, id, cancellationToken))
            {
                slug = $"{originalSlug}-{counter}";
                counter++;
            }
            tag.Slug = slug;

            _unitOfWork.Tags.Update(tag);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return tag;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tag = await GetByIdAsync(id, cancellationToken);
            if (tag == null)
                return false;

            _unitOfWork.Tags.Remove(tag);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tag = await GetByIdAsync(id, cancellationToken);
            if (tag == null)
                return false;

            _unitOfWork.Tags.SoftDelete(tag);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tag = await _unitOfWork.Tags.GetByIdAsync(id, cancellationToken);
            if (tag == null)
                return false;

            _unitOfWork.Tags.Restore(tag);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        // Tag management
        public async Task<IEnumerable<Tag>> GetOrCreateTagsAsync(IEnumerable<string> tagNames, CancellationToken cancellationToken = default)
        {
            var tags = new List<Tag>();

            foreach (var tagName in tagNames)
            {
                var tag = await GetByNameAsync(tagName, cancellationToken);
                if (tag == null)
                {
                    tag = await CreateAsync(tagName, cancellationToken);
                }
                tags.Add(tag);
            }

            return tags;
        }

        public async Task<IEnumerable<Tag>> ParseTagsAsync(string tagString, char separator = ',', CancellationToken cancellationToken = default)
        {
            var tagNames = tagString
                .Split(separator)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct();

            return await GetOrCreateTagsAsync(tagNames, cancellationToken);
        }

        // Validation
        public async Task<bool> NameExistsAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = _unitOfWork.Tags.FindAsync(t => t.Name == name, cancellationToken);
            var result = await query;

            if (excludeId.HasValue)
                return result.Any(t => t.Id != excludeId.Value);

            return result.Any();
        }

        public async Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = _unitOfWork.Tags.FindAsync(t => t.Slug == slug, cancellationToken);
            var result = await query;

            if (excludeId.HasValue)
                return result.Any(t => t.Id != excludeId.Value);

            return result.Any();
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Tags.AnyAsync(t => t.Id == id, cancellationToken);
        }
    }
}
