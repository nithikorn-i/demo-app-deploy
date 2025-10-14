using System;
using System.ComponentModel.DataAnnotations;
using Application.Interfaces.SU;
using Application.Models;
using Application.Models.SU;
using Domain.Entities.SU;
using Infrastructure.Repositories.SU;

namespace Application.Services.SU;


public class WinService(IWinRepository winRepository) : IWinService
{
    public IWinRepository _winRepository = winRepository;

    public async Task<PaginatedResult<WinDto>> GetAllUsers(int page, int pageSize)
    {
        var (user, total) = await _winRepository.GetAllUsers(page, pageSize);
        return new PaginatedResult<WinDto>
        {
            Result = user.Select(x => new WinDto
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

    public async Task<WinDto?> GetUserById(Guid id)
    {
        var user = await _winRepository.GetUserById(id);
        if (user == null) return null;
        return new WinDto
        {
            Fullname = user.Fullname,
            Email = user.Email
        };
    }

    public async Task<WinDto> CreateUser(WinDto WinDto)
    {
        var dataUser = new Win
        {
            Fullname = WinDto.Fullname,
            Email = WinDto.Email
        };
        var user = await _winRepository.CreateUser(dataUser);
        return new WinDto
        {
            Fullname = user.Fullname,
            Email = user.Email
        };
    }

    public async Task<WinDto?> UpdateUser(Guid id, string fullname, string email)
    {
        var updatedUser = await _winRepository.UpdateUser(id, fullname, email);
        if (updatedUser == null) return null;
        return new WinDto
        {
            Id = updatedUser.Id,
            Fullname = updatedUser.Fullname,
            Email = updatedUser.Email
        };
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        return await _winRepository.DeleteUser(id);
    }
}
