using System;
using System.ComponentModel.DataAnnotations;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Models.ViewModels
{
    /// <summary>
    /// View model for creating and editing blog posts
    /// </summary>
    public class BlogPostViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
        [Display(Name = "Post Title")]
        public string Title { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Slug cannot exceed 300 characters")]
        [Display(Name = "URL Slug")]
        public string? Slug { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [Display(Name = "Post Content")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Summary cannot exceed 500 characters")]
        [Display(Name = "Summary")]
        [DataType(DataType.MultilineText)]
        public string? Summary { get; set; }

        [StringLength(200, ErrorMessage = "Author name cannot exceed 200 characters")]
        [Display(Name = "Author Name")]
        public string? Author { get; set; }

        [Url(ErrorMessage = "Please enter a valid Image URL")]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Url(ErrorMessage = "Please enter a valid Featured Image URL")]
        [Display(Name = "Featured Image URL")]
        public string? FeaturedImageUrl { get; set; }

        [Display(Name = "Post Status")]
        public BlogPostStatus Status { get; set; } = BlogPostStatus.Draft;

        [Display(Name = "Publish Date")]
        [DataType(DataType.DateTime)]
        public DateTime? PublishedAt { get; set; }

        [Display(Name = "Schedule Publish Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ScheduledPublishAt { get; set; }

        [StringLength(1000, ErrorMessage = "Tags cannot exceed 1000 characters")]
        [Display(Name = "Tags (comma-separated)")]
        public string? Tags { get; set; }

        [StringLength(200, ErrorMessage = "Category cannot exceed 200 characters")]
        [Display(Name = "Category")]
        public string? Category { get; set; }

        [Display(Name = "Reading Time (minutes)")]
        [Range(0, 999, ErrorMessage = "Reading time must be between 0 and 999 minutes")]
        public int ReadingTimeMinutes { get; set; } = 0;

        [Display(Name = "Allow Comments")]
        public bool AllowComments { get; set; } = true;

        [Display(Name = "Featured Post")]
        public bool IsFeatured { get; set; } = false;

        [StringLength(200, ErrorMessage = "Meta title cannot exceed 200 characters")]
        [Display(Name = "SEO Title")]
        public string? MetaTitle { get; set; }

        [StringLength(500, ErrorMessage = "Meta description cannot exceed 500 characters")]
        [Display(Name = "SEO Description")]
        [DataType(DataType.MultilineText)]
        public string? MetaDescription { get; set; }

        [StringLength(500, ErrorMessage = "Meta keywords cannot exceed 500 characters")]
        [Display(Name = "SEO Keywords")]
        public string? MetaKeywords { get; set; }
    }
}
