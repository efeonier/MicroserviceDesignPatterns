using Shared.SagaOrchestration.Interface;

namespace Shared.SagaOrchestration.Concrete;

public class OrderRequestCompletedEvent: IOrderRequestCompletedEvent
{
    public int OrderId { get; set; }
}