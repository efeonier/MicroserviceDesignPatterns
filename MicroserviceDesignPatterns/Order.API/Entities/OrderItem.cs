using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.API.Entities;

public class OrderItem : IEntity
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [ForeignKey("OrderId")]
    public int OrderId { get; set; }

    public int Count { get; set; }
    public Order Order { get; set; }
}