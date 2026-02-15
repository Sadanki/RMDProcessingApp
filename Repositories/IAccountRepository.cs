using System.Collections.Generic;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetByParticipant(int participantId);
    }
}
