using System;
using System.ComponentModel.DataAnnotations;

namespace RMDProcessingApp.Models
{
    public class SystemConfiguration
    {
        [Key]
        public string ConfigKey { get; set; } = string.Empty;

        public string ConfigValue { get; set; } = string.Empty;

        public DateTime EffectiveDate { get; set; } = DateTime.UtcNow;
    }
}
