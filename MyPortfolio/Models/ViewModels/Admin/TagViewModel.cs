using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models.ViewModels.Admin
{
    public class TagViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Tag Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug is required")]
        [StringLength(80)]
        [Display(Name = "URL Slug")]
        [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Slug must contain only lowercase letters, numbers, and hyphens")]
        public string Slug { get; set; } = string.Empty;

        [StringLength(200)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [StringLength(50)]
        [Display(Name = "Color (Hex)")]
        public string? Color { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}
