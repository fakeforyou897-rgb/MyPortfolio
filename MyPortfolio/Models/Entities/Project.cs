using System;
using System.ComponentModel.DataAnnotations;
using MyPortfolio.Models.Common;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Models.Entities
{
    /// <summary>
    /// Represents a portfolio project
    /// </summary>
    public class Project : BaseEntity
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public required string Title { get; set; }

        [Required]
        [StringLength(250)]
        public required string Slug { get; set; }

        [StringLength(500)]
        public string? ShortDescription { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public required string Description { get; set; }

        [Required]
        [StringLength(500)]
        public required string Technologies { get; set; }

        [Url]
        [StringLength(500)]
        public string? ImageUrl { get; set; }

        [Url]
        [StringLength(500)]
        public string? ThumbnailUrl { get; set; }

        [Url]
        [StringLength(500)]
        public string? GitHubUrl { get; set; }

        [Url]
        [StringLength(500)]
        public string? LiveDemoUrl { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.Completed;

        public Priority Priority { get; set; } = Priority.Medium;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int DisplayOrder { get; set; } = 0;

        public bool IsFeatured { get; set; } = false;

        public bool IsPublished { get; set; } = true;

        [Range(1, 5)]
        public int? Rating { get; set; }

        public int ViewCount { get; set; } = 0;

        public int LikeCount { get; set; } = 0;

        [StringLength(100)]
        public string? ClientName { get; set; }

        [StringLength(500)]
        public string? ClientUrl { get; set; }

        public Guid? CategoryId { get; set; }

        // Navigation properties
        public virtual Category? Category { get; set; }
        public virtual ICollection<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();
    }
}
