using MassTransit;
using Payment.API.Enums;
using Payment.API.Repositories.Interface;
using Shared;

namespace Payment.API.Consumers;

public class StockReservedEventConsumer(ILogger<StockReservedEventConsumer> logger, IPublishEndpoint publishEndpoint, IPaymentRepository paymentRepository) : IConsumer<StockReservedEvent>
{
    private const decimal Balance = 3000m;
    public async Task Consume(ConsumeContext<StockReservedEvent> context)
    {
        if (Balance > context.Message.Payment.TotalPrice)
        {
            var payment = new Entities.Payment()
            {
                BuyerId = context.Message.BuyerId, OrderId = context.Message.OrderId, PaymentTotal = context.Message.Payment.TotalPrice, Status = PaymentStatus.Success,
            };
            await paymentRepository.AddAsync(payment);
            logger.LogInformation("{PaymentTotalPrice} TL was withdrawn from credit card for user Id:{MessageBuyerId}", context.Message.Payment.TotalPrice, context.Message.BuyerId);
            await publishEndpoint.Publish(new PaymentCompletedEvent() { BuyerId = context.Message.BuyerId, OrderId = context.Message.OrderId });
        }
        else
        {
            var payment = new Entities.Payment()
            {
                BuyerId = context.Message.BuyerId,
                OrderId = context.Message.OrderId,
                PaymentTotal = context.Message.Payment.TotalPrice,
                Status = PaymentStatus.Failed,
                Message = "not enough money"
            };
            await paymentRepository.AddAsync(payment);
            logger.LogInformation("{PaymentTotalPrice} TL was not withdrawn from credit card for user Id:{MessageBuyerId}", context.Message.Payment.TotalPrice, context.Message.BuyerId);
            await publishEndpoint.Publish(new PaymentFailedEvent() { OrderId = context.Message.OrderId, BuyerId = context.Message.BuyerId, Message = "Not withdrawn", OrderItems = context.Message.OrderItems });
        }
    }
}