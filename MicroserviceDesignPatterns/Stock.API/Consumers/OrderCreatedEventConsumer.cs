using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using Stock.API.Repositories.Interface;

namespace Stock.API.Consumers;

public class OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger, IStockRepository stockRepository, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
    : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        List<bool> stockResult = [];
        foreach (var item in context.Message.OrderItems)
        {
            stockResult.Add(await stockRepository.Get().AnyAsync(x => x.ProductId == item.ProductId && x.Quantity > item.Count));
        }
        if (stockResult.TrueForAll(x => x.Equals(true)))
        {
            foreach (var item in context.Message.OrderItems)
            {
                var stock = await stockRepository.GetByIdAsync(item.ProductId);
                if (stock is not null)
                {
                    stock.Quantity -= item.Count;
                    await stockRepository.UpdateAsync(stock.Id, stock);
                }
            }
            logger.LogInformation("Stock was reserved for buyer Id:{MessageBuyerId}", context.Message.BuyerId);
            var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqSettings.StockReservedEventQueueName}"));
            StockReservedEvent stockReservedEvent = new() { Payment = context.Message.PaymentMessage, BuyerId = context.Message.BuyerId, OrderId = context.Message.OrderId, OrderItems = context.Message.OrderItems };
            await sendEndpoint.Send(stockReservedEvent);
        }
        else
        {
            await publishEndpoint.Publish(new StockNotReservedEvent() { OrderId = context.Message.OrderId, Message = "Not Enough Stock" });
            logger.LogInformation("Not enough stock for buyer Id:{MessageBuyerId}", context.Message.BuyerId);
        }
    }
}