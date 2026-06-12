using System;
using System.ComponentModel.DataAnnotations;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Models.ViewModels
{
    /// <summary>
    /// View model for creating and editing projects
    /// </summary>
    public class ProjectViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
        [Display(Name = "Project Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
        [Display(Name = "Full Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Short description cannot exceed 500 characters")]
        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }

        [Required(ErrorMessage = "Technologies are required")]
        [StringLength(500, ErrorMessage = "Technologies cannot exceed 500 characters")]
        [Display(Name = "Technologies Used")]
        public string Technologies { get; set; } = string.Empty;

        [Url(ErrorMessage = "Please enter a valid URL")]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Url(ErrorMessage = "Please enter a valid GitHub URL")]
        [Display(Name = "GitHub Repository URL")]
        public string? GitHubUrl { get; set; }

        [Url(ErrorMessage = "Please enter a valid Demo URL")]
        [Display(Name = "Live Demo URL")]
        public string? LiveDemoUrl { get; set; }

        [Display(Name = "Project Status")]
        public ProjectStatus Status { get; set; } = ProjectStatus.Completed;

        [Display(Name = "Priority")]
        public Priority Priority { get; set; } = Priority.Medium;

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Display Order")]
        [Range(0, 9999, ErrorMessage = "Display order must be between 0 and 9999")]
        public int DisplayOrder { get; set; } = 0;

        [Display(Name = "Featured Project")]
        public bool IsFeatured { get; set; } = false;

        [Display(Name = "Rating")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Tags cannot exceed 1000 characters")]
        [Display(Name = "Tags (comma-separated)")]
        public string? Tags { get; set; }
    }
}
