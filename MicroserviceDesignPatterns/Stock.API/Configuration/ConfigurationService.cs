using MassTransit;
using Shared;
using Stock.API.Consumer;

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

                cfg.ReceiveEndpoint(RabbitMqSettings.StockReservedEventQueueName,
                e =>
                {
                    e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
                });
            });
        });
        return services;
    }
}