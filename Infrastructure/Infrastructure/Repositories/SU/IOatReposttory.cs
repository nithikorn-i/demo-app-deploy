using System;
using Domian.Entities.SU;

namespace Infrastructure.Repositories.SU;

public interface IOatReposttory
{
    Task<(IEnumerable<Oat>, int)> GetAllOat(int page, int pageSize);
    Task<Oat?> GetOatByID(Guid id);
    Task<Oat> CreateOat(Oat Oat);
    Task<Oat?> UpdateOat(Guid Oat, string fullname, string email);
    Task<bool> DeleteOat(Guid Oat);
}
