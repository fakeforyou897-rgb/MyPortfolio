using System;
using System.ComponentModel.DataAnnotations;
using MyPortfolio.Models.Common;
using MyPortfolio.Models.Enums;

namespace MyPortfolio.Models.Entities
{
    /// <summary>
    /// Represents a contact message from visitors
    /// </summary>
    public class ContactMessage : BaseEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public required string Email { get; set; }

        [StringLength(200)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public required string Subject { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public required string Message { get; set; }

        public MessageStatus Status { get; set; } = MessageStatus.New;

        public Priority Priority { get; set; } = Priority.Medium;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReadAt { get; set; }

        public DateTime? RepliedAt { get; set; }

        [StringLength(2000)]
        public string? Reply { get; set; }

        [StringLength(200)]
        public string? RepliedBy { get; set; }

        [StringLength(500)]
        public string? IpAddress { get; set; }

        [StringLength(500)]
        public string? UserAgent { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public bool IsRead { get; set; } = false;

        public bool IsReplied { get; set; } = false;

        public bool IsSpam { get; set; } = false;
    }
}
