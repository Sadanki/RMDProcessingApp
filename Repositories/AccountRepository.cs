using System.Collections.Generic;
using System.Linq;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        // Demo accounts
        private readonly List<Account> _accounts = new()
        {
            new Account
            {
                AccountId = 1,
                ParticipantId = 1001,
                AccountNumber = "ACC-1001-1",
                AccountType = "Retirement",
                OpeningBalance = 100000,
                CurrentBalance = 120000,
                LastValuationDate = DateTime.Today
            }
        };

        public IEnumerable<Account> GetByParticipant(int participantId) =>
            _accounts.Where(a => a.ParticipantId == participantId);
    }
}
