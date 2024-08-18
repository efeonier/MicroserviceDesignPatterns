using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IStockReservedRequestPayment 
{
    public Guid CorrelationId { get; set; }
    public PaymentMessage PaymentMessage { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; }

    public string BuyerId { get; set; }
}