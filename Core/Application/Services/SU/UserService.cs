using System;
using System.Security.Cryptography.X509Certificates;
using Application.Interfaces.SU;
using Application.Models;
using Application.Models.SU;
using Domian.Entities.SU;
using Infrastructure.Repositories.SU;

namespace Application.Services.SU;

public class UserService(IUserRepository userRepository) : IUserSerice
{
    private IUserRepository _userRepository = userRepository;
    public async Task<User> CreateUser(UserDto userDto)
    {
        var req = new User
        {
            Id = userDto.Id,
            Fullname = userDto.Fullname,
            Email = userDto.Email

        };
        var res = await _userRepository.CreateUser(req);

        return res;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var res = await _userRepository.DeleteUser(id);
        return res;
    }

    public async Task<PaginatedResult<UserDto>> GetAllUsers(int page, int pageSize)
    {
        var (user, total) = await _userRepository.GetAllUser(page, pageSize);
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
        var data = await _userRepository.GetUsersByID(id);
        var res = new UserDto
        {
            Id = data.Id,
            Fullname = data.Fullname,
            Email = data.Email
        };
        return res;
    }

    public async Task<UserDto?> UpdateUser(Guid id, string fullname, string email)
    {
        var data = await _userRepository.UpdateUser(id, fullname, email);

        var user = new UserDto
        {
            Id = data.Id,
            Fullname = data.Fullname,
            Email = data.Email
        };
        return user;
    }
}
