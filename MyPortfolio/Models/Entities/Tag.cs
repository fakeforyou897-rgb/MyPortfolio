using System.ComponentModel.DataAnnotations;
using MyPortfolio.Models.Common;

namespace MyPortfolio.Models.Entities
{
    /// <summary>
    /// Represents a tag for blog posts or projects
    /// </summary>
    public class Tag : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string Name { get; set; }

        [Required]
        [StringLength(80)]
        public required string Slug { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Color { get; set; }

        public int UsageCount { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();
        public virtual ICollection<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();
    }
}
