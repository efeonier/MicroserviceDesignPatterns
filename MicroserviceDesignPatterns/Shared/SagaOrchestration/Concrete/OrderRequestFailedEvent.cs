using Shared.SagaOrchestration.Interface;

namespace Shared.SagaOrchestration.Concrete;

public class OrderRequestFailedEvent : IOrderRequestFailedEvent
{
    public int OrderId { get; set; }
    public string Reason { get; set; }
}