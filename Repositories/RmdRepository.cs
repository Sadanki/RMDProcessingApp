using System.Collections.Generic;
using System.Linq;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public class RmdRepository : IRmdRepository
    {
        private readonly List<Rmd> _rmds = new();
        private int _nextId = 1;

        public Rmd Add(Rmd rmd)
        {
            rmd.RmdId = _nextId++;
            _rmds.Add(rmd);
            return rmd;
        }

        public Rmd? GetById(int id) =>
            _rmds.FirstOrDefault(r => r.RmdId == id);

        public IEnumerable<Rmd> GetByParticipant(int participantId) =>
            _rmds.Where(r => r.ParticipantId == participantId);

        public void Update(Rmd rmd)
        {
            var existing = GetById(rmd.RmdId);
            if (existing == null) return;

            existing.OpeningBalance = rmd.OpeningBalance;
            existing.CalculatedAmount = rmd.CalculatedAmount;
            existing.Status = rmd.Status;
            existing.ApprovedDate = rmd.ApprovedDate;
            existing.LockedDate = rmd.LockedDate;
            existing.FinancialYear = rmd.FinancialYear;
        }
    }
}
