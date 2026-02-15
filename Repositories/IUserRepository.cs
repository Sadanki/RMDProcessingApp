using System.Collections.Generic;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        IEnumerable<User> GetAll();
    }
}
