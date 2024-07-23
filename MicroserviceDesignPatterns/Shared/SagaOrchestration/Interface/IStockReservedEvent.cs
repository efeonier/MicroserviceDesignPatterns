using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IStockReservedEvent : CorrelatedBy<Guid>
{
    public List<OrderItemMessage> OrderItems { get; set; }
}