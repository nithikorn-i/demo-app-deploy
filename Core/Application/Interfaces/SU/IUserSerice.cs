using System;
using Application.Models;
using Application.Models.SU;
using Domian.Entities.SU;

namespace Application.Interfaces.SU;

public interface IUserSerice
{
    public Task<PaginatedResult<UserDto>> GetAllUsers(int page, int pageSize);

    public Task<UserDto?> GetUserById(Guid id);

    public Task<User> CreateUser(UserDto userDto);

    public Task<UserDto?> UpdateUser(Guid id, string fullname, string email);

    public Task<bool> DeleteUser(Guid id);
}
