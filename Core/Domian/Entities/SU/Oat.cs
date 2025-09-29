using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domian.Entities.SU;

[Table("Oat")]
public class Oat
{
    public Guid Id { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public void Deconstruct(out object user, out object total)
    {
        throw new NotImplementedException();
    }
}
