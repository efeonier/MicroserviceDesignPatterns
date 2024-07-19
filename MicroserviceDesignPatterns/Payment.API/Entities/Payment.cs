using Payment.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.API.Entities;

public class Payment : IEntity
{
    public int Id { get; set; }

    [MaxLength(500)]
    public string BuyerId { get; set; } = string.Empty;

    public int OrderId { get; set; }
    public PaymentStatus Status { get; set; }

    [MaxLength(250)]
    public string Message { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal PaymentTotal { get; set; }
}