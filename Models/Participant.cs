using System.ComponentModel.DataAnnotations;

namespace RMDProcessingApp.Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Plan ID is required")]
        public string PlanId { get; set; }

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; set; }

        // IMPORTANT: no [Required], nullable
        public string? RmdStatus { get; set; }
    }
}
