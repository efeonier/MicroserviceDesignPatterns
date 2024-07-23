using MassTransit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace SagaStateMachineWorkerService.Entities;

public class OrderStateInstance : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public string BuyerId { get; set; }
    public int OrderId { get; set; }

    [MaxLength(100)]
    public string CardName { get; set; } = string.Empty;

    [MaxLength(150)]
    public string CardNumber { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Expiration { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Cvv { get; set; } = string.Empty;

    [Column(TypeName="decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    public DateTime CreateDate { get; set; }

    public override string ToString()
    {
        PropertyInfo[] properties = GetType().GetProperties();
        var stringBuilder = new StringBuilder();
        properties.ToList().ForEach(p =>
        {
            object value = p.GetValue(this, null);
            stringBuilder.AppendLine($"{p.Name}:{value}");
        });
        stringBuilder.AppendLine("-----------------------");
        return stringBuilder.ToString();
    }
}