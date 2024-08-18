using MassTransit;
using Order.API.Consumers;
using Shared;

namespace Order.API.Configuration;

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
            x.AddConsumer<OrderRequestCompletedEventConsumer>();
            x.AddConsumer<OrderRequestFailedEventConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(url,
                host,
                c =>
                {
                    c.Username(userName ?? string.Empty);
                    c.Password(password ?? string.Empty);
                });

                cfg.ReceiveEndpoint(RabbitMqSettings.OrderRequestCompletedEventtQueueName,
                x =>
                {
                    x.ConfigureConsumer<OrderRequestCompletedEventConsumer>(ctx);
                });

                cfg.ReceiveEndpoint(RabbitMqSettings.OrderRequestFailedEventtQueueName,
                x =>
                {
                    x.ConfigureConsumer<OrderRequestFailedEventConsumer>(ctx);
                });
            });
        });
        return services;
    }
}