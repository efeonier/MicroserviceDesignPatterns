using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.SagaOrchestration.Concrete;
using Shared.SagaOrchestration.Interface;
using Stock.API.Repositories.Interface;

namespace Stock.API.Consumers;

public class OrderCreatedEventConsumer : IConsumer<IOrderCreatedEvent>
{
    private readonly IStockRepository _stockRepository;
    private ILogger<OrderCreatedEventConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    public OrderCreatedEventConsumer(IStockRepository stockRepository, IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventConsumer> logger)
    {
        _stockRepository = stockRepository;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var stockResult = new List<bool>();

        foreach (var item in context.Message.OrderItems)
        {
            var itemStatus = await _stockRepository.GetByIdAsync(item.ProductId);
            stockResult.Add(itemStatus != null && itemStatus.Quantity > item.Count);
        }

        if (stockResult.TrueForAll(x => x.Equals(true)))
        {
            foreach (var item in context.Message.OrderItems)
            {
                var stock = await _stockRepository.GetByIdAsync(item.ProductId);

                if (stock != null)
                {
                    stock.Quantity -= item.Count;
                    await _stockRepository.UpdateAsync(stock.Id, stock);
                }
            }

            _logger.LogInformation("Stock was reserved for CorrelationId Id :{MessageCorrelationId}", context.Message.CorrelationId);

            StockReservedEvent stockReservedEvent = new(context.Message.CorrelationId) { OrderItems = context.Message.OrderItems };

            await _publishEndpoint.Publish(stockReservedEvent);
        }
        else
        {
            await _publishEndpoint.Publish(new StockNotReservedEvent(context.Message.CorrelationId) { Reason = "Not enough stock" });

            _logger.LogInformation("Not enough stock for CorrelationId Id :{MessageCorrelationId}", context.Message.CorrelationId);
        }
    }
}