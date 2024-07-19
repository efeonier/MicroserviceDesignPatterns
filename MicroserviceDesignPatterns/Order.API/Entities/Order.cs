using Order.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Order.API.Entities;

public class Order : IEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }
    [MaxLength(500)]
    public string BuyerId { get; set; }
    public OrderStatus Status { get; set; }
    [MaxLength(500)]
    public string Message { get; set; } = String.Empty;
    public Address Address { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}