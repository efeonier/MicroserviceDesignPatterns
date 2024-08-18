using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IStockReservedRequestPaymentEvent : CorrelatedBy<Guid>
{
    public PaymentMessage PaymentMessage { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; }

    public string BuyerId { get; set; }
}