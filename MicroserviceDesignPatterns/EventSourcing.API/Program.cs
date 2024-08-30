using EventSourcing.API.EventStores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEventStore(builder.Configuration);
builder.Services.AddSingleton<ProductStream>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();