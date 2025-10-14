using System;
using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories.SU;

public class WinRepository : IWinRepository
{
  private readonly AppDbContext _appDbContext;

  public WinRepository(AppDbContext appDbContext)
  {
    _appDbContext = appDbContext;
  }
  public async Task<(IEnumerable<Win>, int)> GetAllUsers(int page, int pageSize)
  {
    var total = await _appDbContext.Win.CountAsync();
    var users = await _appDbContext.Win.Skip((page - 1) * pageSize).ToListAsync();
    return (users, total);
  }

  public async Task<Win?> GetUserById(Guid id)
  {
    return await _appDbContext.Win.FindAsync();
  }

  public async Task<Win> CreateUser(Win win)
  {
    _appDbContext.Win.Add(win);
    await _appDbContext.SaveChangesAsync();
    return win;
  }

  public async Task<Win?> UpdateUser(Guid id, string fullname, string email)
  {
    var win = await _appDbContext.Win.FindAsync(id);
    if (win == null) return null;

    win.Email = email;
    win.Fullname = fullname;
    _appDbContext.Win.Update(win);
    await _appDbContext.SaveChangesAsync();
    return win;
  }

  public async Task<bool> DeleteUser(Guid id)
  {
    var win = await _appDbContext.Win.FindAsync(id);
    if (win == null) return false;

    _appDbContext.Win.Remove(win);
    await _appDbContext.SaveChangesAsync();
    return true;
  }
}