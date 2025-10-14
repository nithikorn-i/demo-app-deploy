using System;
using Application.Interfaces.SU;
using Application.Models;
using Application.Models.SU;
using Domain.Entities.SU;

namespace Application.Features.SU.User001;

public class Lists(IUserService userService)
{
  private readonly IUserService _userService = userService;

  public async Task<PaginatedResult<UserDto>> GetAllUsers(int page, int pageSize)
  {
    if (page == 0 || pageSize <= 0)
    {
        page = 1;
        pageSize = 10;
    }

    return await _userService.GetAllUsers(page, pageSize);
  }

  public async Task<UserDto?> GetUserById(Guid id)
  {
      return await _userService.GetUserById(id);
  }

  public async Task<UserDto?> CreateUser(UserDto user)
  {
    return await _userService.CreateUser(user);
  }

  public async Task<UserDto?> UpdateUser(Guid id, string fullname, string email)
  {
    return await _userService.UpdateUser(id, fullname, email);
  }
  public async Task<bool> DeleteUser(Guid id)
  {
    return await _userService.DeleteUser(id);
  }

}
