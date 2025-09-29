using System;
using Domian.Entities.SU;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories.SU;

public class UserRepository : IUserRepository
{
    private readonly AppDbcontext _appDbcontext;

    public UserRepository(AppDbcontext appDbcontext)
    {
        _appDbcontext = appDbcontext;
    }

    public async Task<(IEnumerable<User>, int)> GetAllUser(int page, int pageSize)
    {
        var total = await _appDbcontext.Users.CountAsync();
        var users = await _appDbcontext.Users.Skip((page - 1) * pageSize).ToListAsync();
        return (users, total);
    }

    public async Task<User> CreateUser(User user)
    {
        _appDbcontext.Users.Add(user);
        await _appDbcontext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUsersByID(Guid id)
    {
        return await _appDbcontext.Users.FindAsync(id);
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await _appDbcontext.Users.FindAsync(id);
        if (user == null) return false;
        _appDbcontext.Users.Remove(user);
        await _appDbcontext.SaveChangesAsync();

        return true;
    }

    public async Task<User?> UpdateUser(Guid id, string fullname, string email)
    {
        var user = await _appDbcontext.Users.FindAsync(id);
        if (user == null) return null;

        user.Email = email;
        user.Fullname = fullname;
        _appDbcontext.Users.Update(user);
        await _appDbcontext.SaveChangesAsync();
        return user;
    }
}