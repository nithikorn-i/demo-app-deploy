using System;
using Application.Interfaces.SU;
using Application.Models;
using Application.Models.SU;
using Domian.Entities.SU;

namespace Application.Features.SU.User001;

public class Lists(IUserSerice userSerice)
{
    private readonly IUserSerice _userSerice = userSerice;

    public async Task<PaginatedResult<UserDto>> GetAllUsers(int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
        {
            page = 1;
            pageSize = 10;
        }

        var result = await _userSerice.GetAllUsers(page, pageSize);
        Console.WriteLine("Result" + result);
        return result;
    }

    public async Task<UserDto?> GetUserById(Guid id)
    {
        var result = await _userSerice.GetUserById(id);
        Console.WriteLine("Result" + result);
        return result;
    }

    public async Task<User> CreateUser(UserDto userDto)
    {
        var result = await _userSerice.CreateUser(userDto);

        Console.WriteLine("Result" + result);
        return result;
    }

    public async Task<UserDto?> UpdateUser(UserDto userDto)
    {
        var id = userDto.Id;
        var fullname = userDto.Fullname;
        var email = userDto.Email;
        var result = await _userSerice.UpdateUser(id, fullname, email);

        Console.WriteLine("Result" + result);
        return result;
    }
    
    public async Task<bool> DeleteUser(Guid id)
    {
        var result = await _userSerice.DeleteUser(id);
        
        Console.WriteLine("Result" + result);
        return result;
    }
}
