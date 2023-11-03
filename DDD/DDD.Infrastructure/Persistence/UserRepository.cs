using DDD.Application.Common.Interfaces.Persistence;
using DDD.Domain.Entities;

namespace DDD.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly static List<User> _users = new();
        public void Add(User user)
        {
            _users.Add(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User? GetUserEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }
    }
}
