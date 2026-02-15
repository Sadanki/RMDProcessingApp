using System.Collections.Generic;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public interface ISystemConfigurationRepository
    {
        IEnumerable<SystemConfiguration> GetAll();
        void AddOrUpdate(SystemConfiguration config);
    }
}
