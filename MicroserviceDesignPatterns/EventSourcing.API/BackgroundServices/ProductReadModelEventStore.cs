using EventSourcing.API.Context;
using EventSourcing.API.Entities;
using EventSourcing.API.EventStores;
using EventSourcing.Shared.Events.Concretes;
using EventStore.Client;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace EventSourcing.API.BackgroundServices
{
    public class ProductReadModelEventStore : BackgroundService
    {
        private readonly EventStoreClient _eventStoreConnection;
        private readonly ILogger<ProductReadModelEventStore> _logger;
        private readonly IServiceProvider _serviceProvider;
        public ProductReadModelEventStore(EventStoreClient eventStoreConnection, ILogger<ProductReadModelEventStore> logger, IServiceProvider serviceProvider)
        {
            _eventStoreConnection = eventStoreConnection;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventStoreConnection.SubscribeToAllAsync(FromAll.Start, EventAppeared, cancellationToken: stoppingToken);
            throw new NotImplementedException();
        }

        private async Task EventAppeared(StreamSubscription arg1, ResolvedEvent arg2, CancellationToken cancellationToken)
        {
            var type = Type.GetType($"{Encoding.UTF8.GetString(arg2.Event.Metadata.ToArray())}, EventSourcing.Shared");
            _logger.LogInformation("The Message processing... : {S}", type.ToString());
            string eventData = Encoding.UTF8.GetString(arg2.Event.Data.ToArray());

            object? @event = JsonSerializer.Deserialize(eventData, type);

            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Product? product;

            switch (@event)
            {
                case ProductCreatedEvent productCreatedEvent:

                    product = new Product()
                    {
                        Name = productCreatedEvent.Name,
                        Id = productCreatedEvent.Id,
                        Price = productCreatedEvent.Price,
                        Stock = productCreatedEvent.Stock,
                        UserId = productCreatedEvent.UserId
                    };
                    context.Products.Add(product);
                    break;

                case ProductNameChangedEvent productNameChangedEvent:

                    product = await context.Products.FindAsync([productNameChangedEvent.Id], cancellationToken);
                    if (product != null)
                    {
                        product.Name = productNameChangedEvent.ChangedName;
                    }
                    break;

                case ProductPriceChangedEvent productPriceChangedEvent:
                    product = await context.Products.FindAsync([productPriceChangedEvent.Id], cancellationToken);
                    if (product != null)
                    {
                        product.Price = productPriceChangedEvent.ChangedPrice;
                    }
                    break;

                case ProductDeletedEvent productDeletedEvent:
                    product = await context.Products.FindAsync([productDeletedEvent.Id], cancellationToken);
                    if (product != null)
                    {
                        context.Products.Remove(product);
                    }
                    break;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}