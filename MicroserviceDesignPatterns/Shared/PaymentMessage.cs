using System.ComponentModel.DataAnnotations;

namespace Shared;

public class PaymentMessage
{
    [MaxLength(100)]
    public string CardName { get; set; } = string.Empty;

    [MaxLength(150)]
    public string CardNumber { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Expiration { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Cvv { get; set; } = string.Empty;

    public decimal TotalPrice { get; set; }
}