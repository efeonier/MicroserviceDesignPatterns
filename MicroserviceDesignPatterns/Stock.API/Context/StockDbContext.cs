using Microsoft.EntityFrameworkCore;

namespace Stock.API.Context;

public class StockDbContext(DbContextOptions<StockDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("StockDB");
    }
    public DbSet<Entities.Stock> Stocks { get; set; }
}