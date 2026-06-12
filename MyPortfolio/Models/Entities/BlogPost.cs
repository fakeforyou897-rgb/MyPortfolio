using System;
using System.ComponentModel.DataAnnotations;
using MyPortfolio.Models.Common;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Models.Entities
{
    /// <summary>
    /// Represents a blog post
    /// </summary>
    public class BlogPost : BaseEntity
    {
        [Required]
        [StringLength(200, MinimumLength = 5)]
        public required string Title { get; set; }

        [Required]
        [StringLength(300)]
        public required string Slug { get; set; }

        [Required]
        public required string Content { get; set; }

        [StringLength(500)]
        public string? Summary { get; set; }

        [StringLength(200)]
        public string? Author { get; set; }

        [Url]
        [StringLength(500)]
        public string? ImageUrl { get; set; }

        [Url]
        [StringLength(500)]
        public string? FeaturedImageUrl { get; set; }

        public BlogPostStatus Status { get; set; } = BlogPostStatus.Draft;

        public DateTime? PublishedAt { get; set; }

        public DateTime? ScheduledPublishAt { get; set; }

        public int ViewCount { get; set; } = 0;

        public int LikeCount { get; set; } = 0;

        public int CommentCount { get; set; } = 0;

        public int ShareCount { get; set; } = 0;

        public int ReadingTimeMinutes { get; set; } = 0;

        public bool AllowComments { get; set; } = true;

        public bool IsFeatured { get; set; } = false;

        public bool IsPublished { get; set; } = false;

        [StringLength(200)]
        public string? MetaTitle { get; set; }

        [StringLength(500)]
        public string? MetaDescription { get; set; }

        [StringLength(500)]
        public string? MetaKeywords { get; set; }

        public Guid? CategoryId { get; set; }

        // Navigation properties
        public virtual Category? Category { get; set; }
        public virtual ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();
    }
}
