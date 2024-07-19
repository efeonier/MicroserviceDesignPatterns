using MassTransit;
using Shared;
using Stock.API.Repositories.Interface;

namespace Stock.API.Consumers;

public class PaymentFailedEventConsumer(IStockRepository stockRepository, ILogger<PaymentFailedEventConsumer> logger) : IConsumer<PaymentFailedEvent>
{
    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        foreach (var item in context.Message.OrderItems)
        {
            var stock = await stockRepository.GetAsync(w => w.ProductId == item.ProductId);
            if (stock is not null)
            {
                stock.Quantity += item.Count;
                await stockRepository.UpdateAsync(stock.Id, stock);
            }
        }
        logger.LogInformation("Stock was released. For Order Id:{MessageBuyerId}", context.Message.OrderId);
    }
}