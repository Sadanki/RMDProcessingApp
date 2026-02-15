using System;
using System.ComponentModel.DataAnnotations;

namespace RMDProcessingApp.Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2–100 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required, StringLength(50)]
        public string NationalId { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [Required, StringLength(50)]
        public string PlanType { get; set; } = "PLAN-401K";

        [Required, StringLength(50)]
        public string EmploymentStatus { get; set; } = "Active";

        // Active / Retired / Deceased
        [Required, StringLength(20)]
        public string ParticipantStatus { get; set; } = "Active";

        // Convenience (not mapped): computed from DateOfBirth
        public int Age =>
            (int)((DateTime.Today - DateOfBirth).TotalDays / 365.25);
    }
}
