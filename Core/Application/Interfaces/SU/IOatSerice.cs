using Application.Models;
using Application.Models.SU;
using Domian.Entities.SU;

namespace Application.Interfaces.SU;

public interface IOatSerice
{
    public Task<PaginatedResult<OatDto>> GetAllOats(int page, int pageSize);

    public Task<OatDto?> GetOatById(Guid id);

    public Task<Oat> CreateOat(OatDto OatDto);

    public Task<OatDto?> UpdateOat(Guid id, string fullname, string email);

    public Task<bool> DeleteOat(Guid id);
}
