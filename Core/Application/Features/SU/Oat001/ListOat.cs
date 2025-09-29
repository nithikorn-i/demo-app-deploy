using System;
using Application.Interfaces.SU;
using Application.Models;
using Application.Models.SU;
using Domian.Entities.SU;

namespace Application.Features.SU.Oat001;

public class ListOat(IOatSerice OatSerice)
{
    private readonly IOatSerice _OatSerice = OatSerice;

    public async Task<PaginatedResult<OatDto>> GetAllOats(int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
        {
            page = 1;
            pageSize = 10;
        }

        var result = await _OatSerice.GetAllOats(page, pageSize);
        Console.WriteLine("Result" + result);
        return result;
    }

    public async Task<OatDto?> GetOatById(Guid id)
    {
        var result = await _OatSerice.GetOatById(id);
        Console.WriteLine("Result" + result);
        return result;
    }

    public async Task<Oat> CreateOat(OatDto OatDto)
    {
        var result = await _OatSerice.CreateOat(OatDto);

        Console.WriteLine("Result" + result);
        return result;
    }

    public async Task<OatDto?> UpdateOat(OatDto OatDto)
    {
        var id = OatDto.Id;
        var fullname = OatDto.Fullname;
        var email = OatDto.Email;
        var result = await _OatSerice.UpdateOat(id, fullname, email);

        Console.WriteLine("Result" + result);
        return result;
    }
    
    public async Task<bool> DeleteOat(Guid id)
    {
        var result = await _OatSerice.DeleteOat(id);
        
        Console.WriteLine("Result" + result);
        return result;
    }
}
