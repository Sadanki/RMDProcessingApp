using System;
using System.ComponentModel.DataAnnotations;

namespace RMDProcessingApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Admin / Processor / Viewer
        [Required]
        public string Role { get; set; } = "Viewer";

        // Active / Locked
        [Required]
        public string Status { get; set; } = "Active";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginDate { get; set; }
    }
}
