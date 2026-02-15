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

        public IEnumerable<Participant> GetAll() => _participants;

        public Participant? GetById(int id) =>
            _participants.FirstOrDefault(p => p.ParticipantId == id);

        public void Update(Participant participant)
        {
            var existing = GetById(participant.ParticipantId);
            if (existing == null) return;

            existing.FullName = participant.FullName;
            existing.DateOfBirth = participant.DateOfBirth;
            existing.NationalId = participant.NationalId;
            existing.Email = participant.Email;
            existing.Phone = participant.Phone;
            existing.Address = participant.Address;
            existing.PlanType = participant.PlanType;
            existing.EmploymentStatus = participant.EmploymentStatus;
            existing.ParticipantStatus = participant.ParticipantStatus;
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing != null) _participants.Remove(existing);
        }
    }
}
