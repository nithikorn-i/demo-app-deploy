using System;
using Domain.Entities.SU;

namespace Infrastructure.Repositories.SU;

public interface IUserRepository
{
  Task<(IEnumerable<User>, int)> GetAllUsers(int page, int pageSize);
  Task<User?> GetUserById(Guid id);
  Task<User> CreateUser(User user);
  Task<User?> UpdateUser(Guid id, string fullname, string email);
  Task<bool> DeleteUser(Guid id);
}
