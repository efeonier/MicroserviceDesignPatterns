namespace Stock.API.Entities;

public class Stock : IEntity
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}