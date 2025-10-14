using System;
using Application.Interfaces.SU;
using Application.Models;
using Application.Models.SU;

namespace Application.Features.SU.Win001;

public class Lists(IWinService winService)
{
  private readonly IWinService _winService = winService;

  public async Task<PaginatedResult<WinDto>> GetAllUsers(int page, int pageSize)
  {
    if (page == 0 || pageSize <= 0)
    {
        page = 1;
        pageSize = 10;
    }

    return await _winService.GetAllUsers(page, pageSize);
  }

  public async Task<WinDto?> GetUserById(Guid id)
  {
      return await _winService.GetUserById(id);
  }

  public async Task<WinDto> CreateUser(WinDto win)
  {
    return await _winService.CreateUser(win);
  }

  public async Task<WinDto?> UpdateUser(Guid id, string fullname, string email)
  {
    return await _winService.UpdateUser(id, fullname, email);
  }
  public async Task<bool> DeleteUser(Guid id)
  {
    return await _winService.DeleteUser(id);
  }

}
