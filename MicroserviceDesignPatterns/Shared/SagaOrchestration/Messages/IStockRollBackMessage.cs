namespace Shared.SagaOrchestration.Messages;

public interface IStockRollBackMessage
{
    public List<OrderItemMessage> OrderItems { get; set; }
}