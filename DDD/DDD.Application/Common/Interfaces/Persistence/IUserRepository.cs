using DDD.Domain.Entities;

namespace DDD.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetUserEmail(string email);
        void Add(User user);
        IEnumerable<User> GetAll();
    }
}
