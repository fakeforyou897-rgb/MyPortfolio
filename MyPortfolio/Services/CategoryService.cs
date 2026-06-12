using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data.UnitOfWork;
using MyPortfolio.Extensions;
using MyPortfolio.Models.Entities;
using MyPortfolio.Services.Interfaces;

namespace MyPortfolio.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Query operations
        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
        }

        public async Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Slug == slug, cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Categories.GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetParentCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Categories.FindAsync(c => c.ParentCategoryId == null, cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetChildCategoriesAsync(Guid parentId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Categories.FindAsync(c => c.ParentCategoryId == parentId, cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithCountsAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Categories.QueryIncluding(
                c => c.ChildCategories
            ).OrderBy(c => c.DisplayOrder).ToListAsync();
        }

        // Command operations
        public async Task<Category> CreateAsync(string name, string? description = null, Guid? parentId = null, CancellationToken cancellationToken = default)
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

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                Slug = slug,
                Description = description,
                ParentCategoryId = parentId,
                DisplayOrder = 0
            };

            await _unitOfWork.Categories.AddAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category;
        }

        public async Task<Category> UpdateAsync(Guid id, string name, string? description = null, Guid? parentId = null, CancellationToken cancellationToken = default)
        {
            var category = await GetByIdAsync(id, cancellationToken);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {id} not found");

            category.Name = name;
            category.Description = description;
            category.ParentCategoryId = parentId;
            category.Slug = name.ToSlug();

            // Ensure unique slug (excluding current category)
            var slug = category.Slug;
            var counter = 1;
            var originalSlug = slug;
            while (await SlugExistsAsync(slug, id, cancellationToken))
            {
                slug = $"{originalSlug}-{counter}";
                counter++;
            }
            category.Slug = slug;

            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await GetByIdAsync(id, cancellationToken);
            if (category == null)
                return false;

            // Check for children before deleting
            if (await HasChildrenAsync(id, cancellationToken))
                throw new InvalidOperationException("Cannot delete a category that has child categories");

            _unitOfWork.Categories.Remove(category);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await GetByIdAsync(id, cancellationToken);
            if (category == null)
                return false;

            _unitOfWork.Categories.SoftDelete(category);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        public async Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);
            if (category == null)
                return false;

            _unitOfWork.Categories.Restore(category);
            return await _unitOfWork.SaveChangesReturnBoolAsync(cancellationToken);
        }

        // Validation
        public async Task<bool> NameExistsAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = _unitOfWork.Categories.FindAsync(c => c.Name == name, cancellationToken);
            var result = await query;

            if (excludeId.HasValue)
                return result.Any(c => c.Id != excludeId.Value);

            return result.Any();
        }

        public async Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = _unitOfWork.Categories.FindAsync(c => c.Slug == slug, cancellationToken);
            var result = await query;

            if (excludeId.HasValue)
                return result.Any(c => c.Id != excludeId.Value);

            return result.Any();
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Categories.AnyAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<bool> HasChildrenAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Categories.AnyAsync(c => c.ParentCategoryId == id, cancellationToken);
        }
    }
}
