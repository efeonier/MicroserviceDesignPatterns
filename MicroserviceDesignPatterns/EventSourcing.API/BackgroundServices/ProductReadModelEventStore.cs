using EventStore.Client;
namespace EventSourcing.API.BackgroundServices;

public class ProductReadModelEventStore(EventStoreClient eventStoreClient, ILogger<ProductReadModelEventStore> logger, IServiceProvider serviceProvider) : BackgroundService
{

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        return base.StartAsync(cancellationToken);
    }
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        return base.StopAsync(cancellationToken);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await eventStoreClient.SubscribeToAllAsync(FromAll.Start, (subscription, @event, arg3) =>
        {
            
        });
    }
}