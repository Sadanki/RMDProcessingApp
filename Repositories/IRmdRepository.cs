using System.Collections.Generic;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public interface IRmdRepository
    {
        Rmd Add(Rmd rmd);
        Rmd? GetById(int id);
        IEnumerable<Rmd> GetByParticipant(int participantId);
        void Update(Rmd rmd);
    }
}
