using Order.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.API.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string BuyerId { get; set; }
    public OrderStatus Status { get; set; }
    public string Message { get; set; }
    public Address Address { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}