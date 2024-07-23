using Shared.SagaOrchestration.Interface;
using System.ComponentModel.DataAnnotations;

namespace Shared.SagaOrchestration.Concrete;

public class StockNotReservedEvent(Guid correlationId) : IStockNotReservedEvent
{
    [MaxLength(500)]
    public string Message { get; set; } = string.Empty;

    public Guid CorrelationId { get; } = correlationId;
}