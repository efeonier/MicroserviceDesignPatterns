namespace Shared;

public class OrderCreatedEvent
{
    public int OrderId { get; set; }
    public string BuyerId { get; set; } = string.Empty;
    public PaymentMessage PaymentMessage { get; set; } = new();
    public List<OrderItemMessage> OrderItems { get; set; } = [];
}