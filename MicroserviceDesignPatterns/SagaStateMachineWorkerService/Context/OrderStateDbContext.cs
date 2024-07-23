using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagaStateMachineWorkerService.ClassMap;

namespace SagaStateMachineWorkerService.Context;

public class OrderStateDbContext(DbContextOptions options) : SagaDbContext(options)
{
    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new OrderStateMap(); }
    }
}