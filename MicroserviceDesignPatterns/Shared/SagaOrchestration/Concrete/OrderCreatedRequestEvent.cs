using Shared.SagaOrchestration.Interface;

namespace Shared.SagaOrchestration.Concrete;

public class OrderCreatedRequestEvent : IOrderCreatedRequestEvent
{
    public int OrderId { get; set; }
    public string BuyerId { get; set; } = string.Empty;
    public List<OrderItemMessage> OrderItems { get; set; } = [];
    public PaymentMessage Payment { get; set; } = new();
}