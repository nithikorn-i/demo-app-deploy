using System;
using Domian.Entities.SU;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories.SU;

public class OatRepository : IOatReposttory
{
    private readonly AppDbcontext _appDbcontext;

    public OatRepository(AppDbcontext appDbcontext)
    {
        _appDbcontext = appDbcontext;
    }

    public async Task<(IEnumerable<Oat>, int)> GetAllOat(int page, int pageSize)
    {
        var total = await _appDbcontext.Oats.CountAsync();
        var Oats = await _appDbcontext.Oats.Skip((page - 1) * pageSize).ToListAsync();
        return (Oats, total);
    }

    public async Task<Oat> CreateOat(Oat Oat)
    {
        _appDbcontext.Oats.Add(Oat);
        await _appDbcontext.SaveChangesAsync();
        return Oat;
    }

    public async Task<Oat?> GetOatByID(Guid id)
    {
        return await _appDbcontext.Oats.FindAsync(id);
    }

    public async Task<bool> DeleteOat(Guid id)
    {
        var oat = await _appDbcontext.Oats.FindAsync(id);
        if (oat == null) return false;
        _appDbcontext.Oats.Remove(oat);
        await _appDbcontext.SaveChangesAsync();

        return true;
    }

    public async Task<Oat?> UpdateOat(Guid id, string fullname, string email)
    {
        var Oat = await _appDbcontext.Oats.FindAsync(id);
        if (Oat == null) return null;

        Oat.Email = email;
        Oat.Fullname = fullname;
        _appDbcontext.Oats.Update(Oat);
        await _appDbcontext.SaveChangesAsync();
        return Oat;
    }
}
