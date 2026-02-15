using System.Collections.Generic;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public interface IPaymentRepository
    {
        Payment Add(Payment payment);
        IEnumerable<Payment> GetByRmd(int rmdId);
    }
}
