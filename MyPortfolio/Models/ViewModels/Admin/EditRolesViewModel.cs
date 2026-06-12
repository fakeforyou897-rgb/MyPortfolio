using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models.ViewModels.Admin
{
    /// <summary>
    /// View model for editing user roles
    /// </summary>
    public class EditRolesViewModel
    {
        [Required]
        public required string UserId { get; set; }
        
        [Required]
        [EmailAddress]
        public required string UserEmail { get; set; }
        
        public List<RoleSelection> Roles { get; set; } = new();
    }

    /// <summary>
    /// Represents a role selection checkbox
    /// </summary>
    public class RoleSelection
    {
        [Required]
        public required string RoleName { get; set; }
        
        public bool Selected { get; set; }
    }
}
