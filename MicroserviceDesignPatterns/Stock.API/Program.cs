using Microsoft.EntityFrameworkCore;
using Stock.API.Configuration;
using Stock.API.Context;
using Stock.API.Repositories.Concrete;
using Stock.API.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StockDbContext>(options =>
{
    options.UseInMemoryDatabase("StockDB");
});

builder.Services.AddServices(builder.Configuration);
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStockRepository, StockRepository>();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var context = scope.ServiceProvider.GetRequiredService<StockDbContext>();
context.Stocks.Add(new Stock.API.Entities.Stock() { Id = 1, ProductId = 1, Quantity = 100 });
context.Stocks.Add(new Stock.API.Entities.Stock() { Id = 2, ProductId = 2, Quantity = 200 });
await context.SaveChangesAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.RunAsync();