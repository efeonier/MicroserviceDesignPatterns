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
            x.AddConsumer<PaymentCompletedEventConsumer>();
            x.AddConsumer<PaymentFailedEventConsumer>();
            x.AddConsumer<StockNotReservedEventConsumer>();
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(url,
                host,
                c =>
                {
                    c.Username(userName ?? string.Empty);
                    c.Password(password ?? string.Empty);
                });
                cfg.ConfigureEndpoints(ctx);
                cfg.ReceiveEndpoint(RabbitMqSettings.OrderPaymentCompletedEventQueueName,
                e =>
                {
                    e.ConfigureConsumer<PaymentCompletedEventConsumer>(ctx);
                });
                cfg.ReceiveEndpoint(RabbitMqSettings.OrderPaymentFailedEventQueueName,
                e =>
                {
                    e.ConfigureConsumer<PaymentFailedEventConsumer>(ctx);
                });
                cfg.ReceiveEndpoint(RabbitMqSettings.OrderStockNotReservedEventQueueName,
                e =>
                {
                    e.ConfigureConsumer<StockNotReservedEventConsumer>(ctx);
                });
            });
        });
        return services;
    }
}