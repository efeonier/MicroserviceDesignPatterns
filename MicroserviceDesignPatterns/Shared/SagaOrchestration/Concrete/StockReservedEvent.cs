using Shared.SagaOrchestration.Interface;
using System.ComponentModel.DataAnnotations;

namespace Shared.SagaOrchestration.Concrete;

public class StockReservedEvent(Guid correlationId) : IStockReservedEvent
{
    public List<OrderItemMessage> OrderItems { get; set; } = [];
    public Guid CorrelationId { get; } = correlationId;
}