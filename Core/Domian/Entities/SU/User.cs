using System;

namespace Domian.Entities.SU;

public class User
{
    public Guid Id { get; set; }
    public string Fullname { get; set; } = string.Empty;    
    public string Email { get; set; } = string.Empty;

    public void Deconstruct(out object user, out object total)
    {
        throw new NotImplementedException();
    }
}
