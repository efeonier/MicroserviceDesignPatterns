using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IStockNotReservedEvent 
{
    public Guid CorrelationId { get; set; }
    public string Reason { get; set; }
}