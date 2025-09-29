using System;
using Domian.Entities.SU;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbcontext(DbContextOptions<AppDbcontext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Oat> Oats => Set<Oat>();
}
