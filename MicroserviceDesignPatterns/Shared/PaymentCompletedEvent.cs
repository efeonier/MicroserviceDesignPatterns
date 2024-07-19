using System.ComponentModel.DataAnnotations;

namespace Shared;

public class PaymentCompletedEvent
{
    public int OrderId { get; set; }
    [MaxLength(500)]
    public string BuyerId { get; set; } = string.Empty;
}