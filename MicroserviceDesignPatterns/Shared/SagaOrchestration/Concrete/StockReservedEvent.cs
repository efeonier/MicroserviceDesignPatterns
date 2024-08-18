using Shared.SagaOrchestration.Interface;
using System.ComponentModel.DataAnnotations;

namespace Shared.SagaOrchestration.Concrete;

public class StockReservedEvent : IStockReservedEvent
{
    public StockReservedEvent(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public List<OrderItemMessage> OrderItems { get; set; }

    public Guid CorrelationId { get; }
}