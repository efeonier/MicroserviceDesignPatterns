using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IStockNotReservedEvent 
{
    public Guid CorrelationId { get; }
    public string Reason { get; set; }
}