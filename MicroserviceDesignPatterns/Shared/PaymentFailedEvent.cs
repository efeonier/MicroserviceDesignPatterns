using System.ComponentModel.DataAnnotations;

namespace Shared;

public class PaymentFailedEvent
{
    public int OrderId { get; set; }

    [MaxLength(500)]
    public string BuyerId { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Message { get; set; } = string.Empty;
}