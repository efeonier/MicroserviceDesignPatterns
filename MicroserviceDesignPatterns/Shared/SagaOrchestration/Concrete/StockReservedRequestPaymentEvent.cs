using Shared.SagaOrchestration.Interface;

namespace Shared.SagaOrchestration.Concrete;

public class StockReservedRequestPaymentEvent : IStockReservedRequestPaymentEvent
{
    public StockReservedRequestPaymentEvent(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
    public Guid CorrelationId { get; }
    public PaymentMessage PaymentMessage { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; }
    public string BuyerId { get; set; }
}