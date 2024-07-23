using MassTransit;
using Microsoft.EntityFrameworkCore;
using SagaStateMachineWorkerService.Context;
using SagaStateMachineWorkerService.Entities;
using Shared;
using System.Reflection;

namespace SagaStateMachineWorkerService.Configuration;

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
            x.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>()
                .EntityFrameworkRepository(opt =>
                {
                    opt.AddDbContext<DbContext, OrderStateDbContext>((provider, builder) =>
                    {
                        builder.UseSqlServer(configuration.GetConnectionString("SqlServer"),
                        m =>
                        {
                            m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        });
                    });
                });


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
                
                cfg.ReceiveEndpoint(RabbitMqSettings.OrderSaga,
                e =>
                {
                    e.UseMessageRetry(a => a.Immediate(4));
                    e.ConfigureSaga<OrderStateInstance>(ctx);
                });
            });
        });


        return services;
    }
}