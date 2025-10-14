using System;

namespace Application.Models.SU;

public class WinDto
{
  public Guid Id { get; set; }
  public string Fullname { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
}
