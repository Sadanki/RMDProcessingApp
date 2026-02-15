using System;

namespace RMDProcessingApp.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int ParticipantId { get; set; }

        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;

        public decimal OpeningBalance { get; set; }
        public decimal CurrentBalance { get; set; }

        public DateTime LastValuationDate { get; set; }
    }
}
