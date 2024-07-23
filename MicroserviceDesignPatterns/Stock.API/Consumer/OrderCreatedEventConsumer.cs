using MassTransit;
using Shared.SagaOrchestration.Concrete;
using Shared.SagaOrchestration.Interface;
using Stock.API.Repositories.Interface;

namespace Stock.API.Consumer;

public class OrderCreatedEventConsumer(IStockRepository stockRepository, IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventConsumer> logger)
    : IConsumer<IOrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var stockResult = new List<bool>();

        foreach (var item in context.Message.OrderItems)
        {
            var itemStatus = await stockRepository.GetAsync(x => x.ProductId == item.ProductId && x.Quantity > item.Count);
            stockResult.Add(itemStatus != null);
        }

        if (stockResult.TrueForAll(x => x.Equals(true)))
        {
            foreach (var item in context.Message.OrderItems)
            {
                var stock = await stockRepository.GetAsync(x => x.ProductId == item.ProductId);

                if (stock != null)
                {
                    stock.Quantity -= item.Count;
                    await stockRepository.UpdateAsync(stock.Id, stock);
                }
            }

            logger.LogInformation("Stock was reserved for CorrelationId Id :{MessageCorrelationId}", context.Message.CorrelationId);

            StockReservedEvent stockReservedEvent = new(context.Message.CorrelationId) { OrderItems = context.Message.OrderItems };

            await publishEndpoint.Publish(stockReservedEvent);
        }
        else
        {
            await publishEndpoint.Publish(new StockNotReservedEvent(context.Message.CorrelationId) { Message = "Not enough stock" });

            logger.LogInformation("Not enough stock for CorrelationId Id :{MessageCorrelationId}", context.Message.CorrelationId);
        }
    }
}