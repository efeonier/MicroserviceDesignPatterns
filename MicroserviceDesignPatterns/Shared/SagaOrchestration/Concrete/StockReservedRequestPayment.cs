using Shared.SagaOrchestration.Interface;

namespace Shared.SagaOrchestration.Concrete;

public class StockReservedRequestPayment : IStockReservedRequestPayment
{
    public StockReservedRequestPayment(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
    public PaymentMessage PaymentMessage { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; }

    public Guid CorrelationId { get; set; }
    public string BuyerId { get; set; }
}