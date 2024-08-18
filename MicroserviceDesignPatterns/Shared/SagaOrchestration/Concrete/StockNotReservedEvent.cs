using Shared.SagaOrchestration.Interface;
using System.ComponentModel.DataAnnotations;

namespace Shared.SagaOrchestration.Concrete;

public class StockNotReservedEvent : IStockNotReservedEvent
{
    public StockNotReservedEvent(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public string Reason { get; set; }
    public Guid CorrelationId { get; set; }
}
