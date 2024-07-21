using SagaStateMachineWorkerService.WorkerServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWorkerServices(builder.Configuration);

var host = builder.Build();
await host.RunAsync();