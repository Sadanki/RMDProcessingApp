using System;
using System.ComponentModel.DataAnnotations;

namespace RMDProcessingApp.Models
{
    public class RmdProcessing
    {
        public int ProcessingId { get; set; }

        [Required]
        public int RmdId { get; set; }

        // 1 or 2
        [Required]
        public int TurnNumber { get; set; }

        public DateTime ProcessingStartTime { get; set; }
        public DateTime? ProcessingEndTime { get; set; }

        // e.g. Pending / Running / Completed / Stopped
        [Required]
        public string ProcessingStatus { get; set; } = "Pending";
    }
}
