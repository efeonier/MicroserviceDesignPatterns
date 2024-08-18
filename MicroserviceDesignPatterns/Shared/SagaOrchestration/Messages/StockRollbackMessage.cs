namespace Shared.SagaOrchestration.Messages;

public class StockRollbackMessage : IStockRollBackMessage
{
    public List<OrderItemMessage> OrderItems { get; set; }
}