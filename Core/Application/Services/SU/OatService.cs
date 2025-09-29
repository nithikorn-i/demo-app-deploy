using System;
using Application.Interfaces.SU;
using Application.Models;
using Application.Models.SU;
using Domian.Entities.SU;
using Infrastructure.Repositories.SU;

namespace Application.Services.SU;

public class OatService(IOatReposttory OatRepository) : IOatSerice
{
    private IOatReposttory _OatRepository = OatRepository;

    public async Task<Oat> CreateOat(OatDto OatDto)
    {
        var req = new Oat
        {
            Id = OatDto.Id,
            Fullname = OatDto.Fullname,
            Email = OatDto.Email

        };
        var res = await _OatRepository.CreateOat(req);

        return res;
    }

    public async Task<bool> DeleteOat(Guid id)
    {
        var res = await _OatRepository.DeleteOat(id);
        return res;
    }

    public async Task<PaginatedResult<OatDto>> GetAllOats(int page, int pageSize)
    {
        var (Oat, total) = await _OatRepository.GetAllOat(page, pageSize);
        return new PaginatedResult<OatDto>
        {
            Result = Oat.Select(x => new OatDto
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Email = x.Email
            }),
            Page = page,
            PageSize = pageSize,
            TotalCount = total
        };
    }

    public async Task<OatDto?> GetOatById(Guid id)
    {
        var data = await _OatRepository.GetOatByID(id);
        var res = new OatDto
        {
            Id = data.Id,
            Fullname = data.Fullname,
            Email = data.Email
        };
        return res;
    }

    public async Task<OatDto?> UpdateOat(Guid id, string fullname, string email)
    {
        var data = await _OatRepository.UpdateOat(id, fullname, email);

        var Oat = new OatDto
        {
            Id = data.Id,
            Fullname = data.Fullname,
            Email = data.Email
        };
        return Oat;
    }
}
