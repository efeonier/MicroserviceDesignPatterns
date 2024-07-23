using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IStockNotReservedEvent : CorrelatedBy<Guid>
{
    public string Message { get; set; }
}