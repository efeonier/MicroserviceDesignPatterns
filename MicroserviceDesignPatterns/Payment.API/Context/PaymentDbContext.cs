using Microsoft.EntityFrameworkCore;

namespace Payment.API.Context;

public class PaymentDbContext(DbContextOptions<PaymentDbContext> options) : DbContext(options)
{
    public DbSet<Entities.Payment> Payments { get; set; }
}