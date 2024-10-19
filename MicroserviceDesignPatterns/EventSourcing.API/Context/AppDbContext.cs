using EventSourcing.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.API.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}