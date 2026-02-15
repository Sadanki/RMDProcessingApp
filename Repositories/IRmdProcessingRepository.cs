using System.Collections.Generic;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public interface IRmdProcessingRepository
    {
        RmdProcessing Add(RmdProcessing processing);
        IEnumerable<RmdProcessing> GetByRmd(int rmdId);
    }
}
