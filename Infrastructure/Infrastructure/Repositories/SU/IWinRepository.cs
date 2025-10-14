using System;
using Domain.Entities.SU;

namespace Infrastructure.Repositories.SU;

public interface IWinRepository
{
  Task<(IEnumerable<Win>, int)> GetAllUsers(int page, int pageSize);
  Task<Win?> GetUserById(Guid id);
  Task<Win> CreateUser(Win win);
  Task<Win?> UpdateUser(Guid id, string fullname, string email);
  Task<bool> DeleteUser(Guid id);
}
