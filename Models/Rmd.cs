using System;
using System.ComponentModel.DataAnnotations;

namespace RMDProcessingApp.Models
{
    public class Rmd
    {
        public int RmdId { get; set; }

        [Required]
        public int ParticipantId { get; set; }

        [Required]
        public int FinancialYear { get; set; }

        [Required]
        public decimal OpeningBalance { get; set; }

        [Required]
        public decimal CalculatedAmount { get; set; }

        // Draft / Approved / ProcessingTurn1 / ProcessingTurn2 / Completed / Stopped / Cancelled / Locked
        [Required]
        public string Status { get; set; } = "Draft";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovedDate { get; set; }
        public DateTime? LockedDate { get; set; }
    }
}
