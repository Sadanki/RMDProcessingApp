using System.Collections.Generic;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public interface IParticipantRepository
    {
        Participant Add(Participant participant);
        IEnumerable<Participant> GetAll();
        Participant? GetById(int id);
        void Update(Participant participant);
        void Delete(int id);
    }
}
