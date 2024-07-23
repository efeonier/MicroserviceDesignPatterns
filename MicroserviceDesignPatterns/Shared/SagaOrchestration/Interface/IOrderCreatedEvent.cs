using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IOrderCreatedEvent : CorrelatedBy<Guid>
{
    public List<OrderItemMessage> OrderItems { get; set; }
}