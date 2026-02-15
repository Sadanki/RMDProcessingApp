using System.Collections.Generic;
using System.Linq;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly List<Participant> _participants = new();
        private int _nextId = 1001;

        public Participant Add(Participant participant)
        {
            participant.ParticipantId = _nextId++;
            _participants.Add(participant);
            return participant;
        }

        public List<Participant> GetAll()
        {
            return _participants;
        }

        public Participant GetById(int id)
        {
            return _participants.FirstOrDefault(p => p.ParticipantId == id);
        }
    }
}
