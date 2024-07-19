using MassTransit;
using Order.API.Enums;
using Order.API.Repositories.Interface;
using Shared;

namespace Order.API.Consumers;

public class PaymentFailedEventConsumer(IOrderRepository orderRepository, ILogger<PaymentFailedEventConsumer> logger) : IConsumer<PaymentFailedEvent>
{
    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        var order = await orderRepository.GetByIdAsync(context.Message.OrderId);
        if (order is not null)
        {
            order.Status = OrderStatus.Failed;
            order.Message = context.Message.Message;
            await orderRepository.UpdateAsync(order.Id, order);
            logger.LogInformation("Order Id = {MessageOrderId} status changed:{OrderStatus}", context.Message.OrderId, order.Status);
        }
        else
        {
            logger.LogError("Order Id = {MessageOrderId} not found", context.Message.OrderId);
        }
    }
}