using System.ComponentModel.DataAnnotations;

namespace Stock.API.Entities;

public class Stock : IEntity
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }
    public int Quantity { get; set; }
}