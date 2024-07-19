using System.ComponentModel.DataAnnotations;

namespace Shared;

public class StockReservedEvent
{
    public int OrderId { get; set; }

    [MaxLength(100)]
    public string BuyerId { get; set; } = String.Empty;

    public PaymentMessage Payment { get; set; } = new();
    public List<OrderItemMessage> OrderItems { get; set; } = [];
}