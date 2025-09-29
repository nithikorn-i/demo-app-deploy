using System;
using Application.Interfaces.SU;
using Application.Models.SU;

namespace Application.Features.SU.User001;

public class Detail(IUserService userService)
{
  private readonly IUserService _userService = userService;

  public async Task<UserDto?> GetUserById(Guid id)
  {
    return await _userService.GetUserById(id);
  }

  public async Task<UserDto> CreateUser(UserDto userDto)
  {
    return await _userService.CreateUser(userDto);
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
