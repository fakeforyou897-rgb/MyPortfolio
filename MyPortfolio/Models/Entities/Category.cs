using System.ComponentModel.DataAnnotations;
using MyPortfolio.Models.Common;

namespace MyPortfolio.Models.Entities
{
    /// <summary>
    /// Represents a category for blog posts or projects
    /// </summary>
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string Name { get; set; }

        [Required]
        [StringLength(150)]
        public required string Slug { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Color { get; set; }

        [StringLength(50)]
        public string? Icon { get; set; }

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public Guid? ParentCategoryId { get; set; }

        // Navigation properties
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
