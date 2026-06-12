using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models.ViewModels.Admin
{
    public class CategoryViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug is required")]
        [StringLength(150)]
        [Display(Name = "URL Slug")]
        [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Slug must contain only lowercase letters, numbers, and hyphens")]
        public string Slug { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [StringLength(50)]
        [Display(Name = "Color (Hex)")]
        public string? Color { get; set; }

        [StringLength(50)]
        [Display(Name = "Icon Class")]
        public string? Icon { get; set; }

        [Display(Name = "Display Order")]
        [Range(0, 9999)]
        public int DisplayOrder { get; set; } = 0;

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Parent Category")]
        public Guid? ParentCategoryId { get; set; }
    }
}
