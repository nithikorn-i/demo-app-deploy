using System;

namespace Domain.Entities.SU;

public class Win
{
  public Guid Id { get; set; }
  public string Fullname { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
}

