using MassTransit;
using Order.API.Enums;
using Order.API.Repositories.Interface;
using Shared.SagaOrchestration.Interface;

namespace Order.API.Consumers;

public class OrderRequestFailedEventConsumer : IConsumer<IOrderRequestFailedEvent>
{
    private readonly IOrderRepository _orderRepository;

    private readonly ILogger<OrderRequestFailedEventConsumer> _logger;
    public OrderRequestFailedEventConsumer(IOrderRepository orderRepository, ILogger<OrderRequestFailedEventConsumer> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<IOrderRequestFailedEvent> context)
    {
        var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);

        if (order != null)
        {
            order.Status = OrderStatus.Failed;
            order.Message = context.Message.Reason;
            await _orderRepository.UpdateAsync(order.Id, order);

            _logger.LogInformation("Order (Id={MessageOrderId}) status changed : {OrderStatus}", context.Message.OrderId, order.Status);
        }
        else
        {
            _logger.LogError("Order (Id={MessageOrderId}) not found", context.Message.OrderId);
        }
    }
}