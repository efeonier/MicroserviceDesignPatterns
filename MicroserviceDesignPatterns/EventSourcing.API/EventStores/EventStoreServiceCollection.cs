namespace EventSourcing.API.EventStores;

public static class EventStoreServiceCollection
{
    public static IServiceCollection AddEventStore(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("EventStore");
        services.AddEventStoreClient(connectionString ?? string.Empty);
        using var logFactory = LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Information);
            builder.AddConsole();
        });
        configuration.Bind("EventStore", logFactory);
        return services;
    }
}