using System.Collections.Generic;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public interface IParticipantRepository
    {
        Participant Add(Participant participant);
        List<Participant> GetAll();
        Participant GetById(int id);
    }
}
