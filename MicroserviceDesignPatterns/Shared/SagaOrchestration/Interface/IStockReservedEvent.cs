using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IStockReservedEvent
{
    public Guid CorrelationId { get; }
    public List<OrderItemMessage> OrderItems { get; set; }
}