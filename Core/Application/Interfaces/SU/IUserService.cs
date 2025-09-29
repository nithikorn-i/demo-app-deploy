using System;
using Application.Models;
using Application.Models.SU;

namespace Application.Interfaces.SU;

public interface IUserService
{
  public Task<PaginatedResult<UserDto>> GetAllUsers(int page, int pageSize);
  public Task<UserDto?> GetUserById(Guid id);
  public Task<UserDto> CreateUser(UserDto userDto);
  public Task<UserDto?> UpdateUser(Guid id, string fullname, string email);
  public Task<bool> DeleteUser(Guid id);
}
