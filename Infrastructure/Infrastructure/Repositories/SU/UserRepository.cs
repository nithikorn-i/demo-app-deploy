using System;
using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories.SU;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _appDbContext;

  public UserRepository(AppDbContext appDbContext)
  {
    _appDbContext = appDbContext;
  }
  public async Task<(IEnumerable<User>, int)> GetAllUsers(int page, int pageSize)
  {
    var total = await _appDbContext.Users.CountAsync();
    var users = await _appDbContext.Users.Skip((page - 1) * pageSize).ToListAsync();
    return (users, total);
  }

  public async Task<User?> GetUserById(Guid id)
  {
    return await _appDbContext.Users.FindAsync();
  }

  public async Task<User> CreateUser(User user)
  {
    _appDbContext.Users.Add(user);
    await _appDbContext.SaveChangesAsync();
    return user;
  }

  public async Task<User?> UpdateUser(Guid id, string fullname, string email)
  {
    var user = await _appDbContext.Users.FindAsync(id);
    if (user == null) return null;

    user.Email = email;
    user.Fullname = fullname;
    _appDbContext.Users.Update(user);
    await _appDbContext.SaveChangesAsync();
    return user;
  }

  public async Task<bool> DeleteUser(Guid id)
  {
    var users = await _appDbContext.Users.FindAsync(id);
    if (users == null) return false;

    _appDbContext.Users.Remove(users);
    await _appDbContext.SaveChangesAsync();
    return true;
  }
}
