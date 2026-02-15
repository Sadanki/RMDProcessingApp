using System.Collections.Generic;
using System.Linq;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public class RmdProcessingRepository : IRmdProcessingRepository
    {
        private readonly List<RmdProcessing> _items = new();
        private int _nextId = 1;

        public RmdProcessing Add(RmdProcessing processing)
        {
            processing.ProcessingId = _nextId++;
            _items.Add(processing);
            return processing;
        }

        public IEnumerable<RmdProcessing> GetByRmd(int rmdId) =>
            _items.Where(p => p.RmdId == rmdId);
    }
}
