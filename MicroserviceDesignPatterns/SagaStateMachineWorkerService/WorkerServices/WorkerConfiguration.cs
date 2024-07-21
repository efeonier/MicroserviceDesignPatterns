namespace SagaStateMachineWorkerService.WorkerServices;

public static class WorkerConfiguration
{
    public static IServiceCollection AddWorkerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<Worker>();

        return services;
    }
}