using EventStore.Client;

namespace EventSourcing.API.EventStores;

public static class EventStoreServiceCollection
{
    public static void AddEventStore(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("EventStore") ?? string.Empty;
        services.AddEventStoreClient(connectionString,
        s =>
        {
            s.ConnectionName = "EventStoreApi";
            s.LoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            s.ConnectivitySettings.Insecure = true;
        });
        services.AddSingleton<ProductStream>();
    }

}