using System;
using System.ComponentModel.DataAnnotations;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Models.ViewModels
{
    /// <summary>
    /// View model for contact form submission
    /// </summary>
    public class ContactMessageViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [Display(Name = "Your Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(200, ErrorMessage = "Phone cannot exceed 200 characters")]
        [Display(Name = "Phone Number (Optional)")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Subject must be between 3 and 150 characters")]
        [Display(Name = "Subject")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 2000 characters")]
        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// View model for admin to manage contact messages
    /// </summary>
    public class ContactMessageAdminViewModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        
        public string? Phone { get; set; }
        
        public string Subject { get; set; } = string.Empty;
        
        public string Message { get; set; } = string.Empty;
        
        public MessageStatus Status { get; set; }
        
        public Priority Priority { get; set; }
        
        public DateTime SentAt { get; set; }
        
        public DateTime? ReadAt { get; set; }
        
        public DateTime? RepliedAt { get; set; }
        
        [StringLength(2000, ErrorMessage = "Reply cannot exceed 2000 characters")]
        [Display(Name = "Reply Message")]
        [DataType(DataType.MultilineText)]
        public string? Reply { get; set; }
        
        public string? RepliedBy { get; set; }
        
        public string? IpAddress { get; set; }
        
        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        [Display(Name = "Internal Notes")]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }
        
        public bool IsRead { get; set; }
        
        public bool IsReplied { get; set; }
        
        public bool IsSpam { get; set; }
    }
}
