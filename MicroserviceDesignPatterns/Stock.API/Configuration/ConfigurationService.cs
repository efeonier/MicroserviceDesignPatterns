using MassTransit;
using Shared;
using Stock.API.Consumers;

namespace Stock.API.Configuration;

public static class ConfigurationService
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var massTransitSection = configuration.GetSection("MassTransit");
        string url = massTransitSection.GetValue<string>("Url");
        string host = massTransitSection.GetValue<string>("Host");
        string userName = massTransitSection.GetValue<string>("UserName");
        string password = massTransitSection.GetValue<string>("Password");
        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderCreatedEventConsumer>();
            x.AddConsumer<PaymentFailedEventConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(url,
                host,
                c =>
                {
                    c.Username(userName ?? string.Empty);
                    c.Password(password ?? string.Empty);
                });
                cfg.ConfigureEndpoints(context);
                cfg.ReceiveEndpoint(RabbitMqSettings.StockOrderCreatedEventQueueName,
                e =>
                {
                    e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
                });
                cfg.ReceiveEndpoint(RabbitMqSettings.StockPaymentFailedEventQueueName,
                e =>
                {
                    e.ConfigureConsumer<PaymentFailedEventConsumer>(context);
                });
            });

            
        });
        return services;
    }
}