using SagaStateMachineWorkerService.Configuration;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddServices(builder.Configuration);
builder.Services.AddWorkerServices(builder.Configuration);

var host = builder.Build();
await host.RunAsync();