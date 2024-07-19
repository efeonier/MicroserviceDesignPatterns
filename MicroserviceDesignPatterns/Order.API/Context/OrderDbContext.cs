using Microsoft.EntityFrameworkCore;
using Order.API.Entities;

namespace Order.API.Context;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Entities.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}