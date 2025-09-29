using System;
using Domian.Entities.SU;

namespace Infrastructure.Repositories.SU;

public interface IUserRepository
{
    Task<(IEnumerable<User>, int)> GetAllUser(int page, int pageSize);
    Task<User?> GetUsersByID(Guid id);
    Task<User> CreateUser(User user);
    Task<User?> UpdateUser(Guid user, string fullname, string email);
    Task<bool> DeleteUser(Guid user);
}
