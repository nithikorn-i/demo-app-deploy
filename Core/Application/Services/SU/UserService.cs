using System;
using System.ComponentModel.DataAnnotations;
using Application.Interfaces.SU;
using Application.Models;
using Application.Models.SU;
using Domain.Entities.SU;
using Infrastructure.Repositories.SU;

namespace Application.Services.SU;

public class UserService(IUserRepository userRepository) : IUserService
{
  public IUserRepository _userRepository = userRepository;

  public async Task<PaginatedResult<UserDto>> GetAllUsers(int page, int pageSize)
  {
    var (user, total) = await _userRepository.GetAllUsers(page, pageSize);
    return new PaginatedResult<UserDto>
    {
      Result = user.Select(x => new UserDto
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

  public async Task<UserDto?> GetUserById(Guid id)
  {
    var user = await _userRepository.GetUserById(id);
    if (user == null) return null;
    return new UserDto
    {
      Fullname = user.Fullname,
      Email = user.Email
    };
  }

  public async Task<UserDto> CreateUser(UserDto userDto)
  {
    var dataUser = new User
    {
      Fullname = userDto.Fullname,
      Email = userDto.Email
    };
    var user = await _userRepository.CreateUser(dataUser);
    return new UserDto
    {
      Fullname = user.Fullname,
      Email = user.Email
    };
  }

  public async Task<UserDto?> UpdateUser(Guid id, string fullname, string email)
  {
    var updatedUser  = await _userRepository.UpdateUser(id, fullname, email);
    if (updatedUser == null) return null;
    return new UserDto
    {
      Id = updatedUser.Id,
      Fullname = updatedUser.Fullname,
      Email = updatedUser.Email
    };
  }

  public async Task<bool> DeleteUser(Guid id)
  {
    return await _userRepository.DeleteUser(id);
  }
}
