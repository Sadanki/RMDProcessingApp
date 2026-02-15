using System.Collections.Generic;
using System.Linq;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly List<Payment> _payments = new();
        private int _nextId = 1;

        public Payment Add(Payment payment)
        {
            payment.PaymentId = _nextId++;
            _payments.Add(payment);
            return payment;
        }

        public IEnumerable<Payment> GetByRmd(int rmdId) =>
            _payments.Where(p => p.RmdId == rmdId);
    }
}
