using System;
using Application.Models;
using Application.Models.SU;

namespace Application.Interfaces.SU;

public interface IWinService
{
  public Task<PaginatedResult<WinDto>> GetAllUsers(int page, int pageSize);
  public Task<WinDto?> GetUserById(Guid id);
  public Task<WinDto> CreateUser(WinDto WinDto);
  public Task<WinDto?> UpdateUser(Guid id, string fullname, string email);
  public Task<bool> DeleteUser(Guid id);
}
