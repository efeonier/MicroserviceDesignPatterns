using MassTransit;
using Order.API.Enums;
using Order.API.Repositories.Interface;
using Shared.SagaOrchestration.Interface;

namespace Order.API.Consumers;

public class OrderRequestCompletedEventConsumer : IConsumer<IOrderRequestCompletedEvent>
{
    private readonly IOrderRepository _orderRepository;

    private readonly ILogger<OrderRequestCompletedEventConsumer> _logger;

    public OrderRequestCompletedEventConsumer(IOrderRepository orderRepository, ILogger<OrderRequestCompletedEventConsumer> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<IOrderRequestCompletedEvent> context)
    {
        var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);

        if (order != null)
        {
            order.Status = OrderStatus.Completed;
            await _orderRepository.UpdateAsync(order.Id, order);

            _logger.LogInformation("Order (Id={MessageOrderId}) status changed : {OrderStatus}", context.Message.OrderId, order.Status);
        }
        else
        {
            _logger.LogError("Order (Id={MessageOrderId}) not found", context.Message.OrderId);
        }
    }
}