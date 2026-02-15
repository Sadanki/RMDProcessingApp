using System.Collections.Generic;
using System.Linq;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new()
        {
            new User { UserId = 1, Name = "Admin User", Email = "admin@example.com", Role = "Admin" },
            new User { UserId = 2, Name = "Processor User", Email = "processor@example.com", Role = "Processor" },
            new User { UserId = 3, Name = "Viewer User", Email = "viewer@example.com", Role = "Viewer" }
        };

        public User? GetByEmail(string email) =>
            _users.FirstOrDefault(u => u.Email == email);

        public IEnumerable<User> GetAll() => _users;
    }
}
