using System.Collections.Generic;
using System.Linq;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public class SystemConfigurationRepository : ISystemConfigurationRepository
    {
        private readonly List<SystemConfiguration> _configs = new()
        {
            new SystemConfiguration { ConfigKey = "Cutoff_Turn1_End", ConfigValue = "14:00" },
            new SystemConfiguration { ConfigKey = "Cutoff_Turn2_End", ConfigValue = "17:00" }
        };

        public IEnumerable<SystemConfiguration> GetAll() => _configs;

        public void AddOrUpdate(SystemConfiguration config)
        {
            var existing = _configs.FirstOrDefault(c => c.ConfigKey == config.ConfigKey);
            if (existing == null)
            {
                _configs.Add(config);
            }
            else
            {
                existing.ConfigValue = config.ConfigValue;
                existing.EffectiveDate = config.EffectiveDate;
            }
        }
    }
}
