using MassTransit;
using Shared;
using Shared.SagaOrchestration.Concrete;
using Shared.SagaOrchestration.Interface;

namespace SagaStateMachineWorkerService.Entities;

public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
{
    public Event<IOrderCreatedRequestEvent> OrderCreatedRequestEvent { get; set; }
    public Event<IStockReservedEvent> StockReservedEvent { get; set; }
    public State OrderCreated { get; private set; }
    public State StockReserved { get; private set; }


    public OrderStateMachine()
    {
        InstanceState(x => x.CurrentState);
        Event(() => OrderCreatedRequestEvent,
        y => y.CorrelateBy<int>(x => x.OrderId, z => z.Message.OrderId).SelectId(context => Guid.NewGuid()));

        Event(() => StockReservedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));


        Initially(When(OrderCreatedRequestEvent)
            .Then(context =>
            {
                context.Saga.BuyerId = context.Message.BuyerId;
                context.Saga.OrderId = context.Message.OrderId;
                context.Saga.CreateDate = DateTime.Now;
                context.Saga.CardName = context.Message.Payment.CardName;
                context.Saga.CardNumber = context.Message.Payment.CardNumber;
                context.Saga.Cvv = context.Message.Payment.Cvv;
                context.Saga.Expiration = context.Message.Payment.Expiration;
                context.Saga.TotalPrice = context.Message.Payment.TotalPrice;
            })
            .Then(context =>
            {
                Console.WriteLine($"OrderCreatedRequestEvent before:{context.Saga}");
            })
            .Publish(context => new OrderCreatedEvent(context.Saga.CorrelationId) { OrderItems = context.Message.OrderItems })
            .TransitionTo(OrderCreated)
            .Then(context =>
            {
                Console.WriteLine($"OrderCreatedRequestEvent after:{context.Saga}");
            }));

        During(OrderCreated,
        When(StockReservedEvent)
            .TransitionTo(StockReserved)
            .Send(new Uri($"queue:{RabbitMqSettings.PaymentStockReservedRequestQueueName}"),
            context =>
                new StockReservedRequestPaymentEvent(context.Saga.CorrelationId)
                {
                    BuyerId = context.Saga.BuyerId,
                    OrderItems = context.Message.OrderItems,
                    PaymentMessage = new PaymentMessage()
                    {
                        CardName = context.Saga.CardName,
                        CardNumber = context.Saga.CardNumber,
                        Cvv = context.Saga.Cvv,
                        Expiration = context.Saga.Expiration,
                        TotalPrice = context.Saga.TotalPrice
                    }
                })
            .Then(context =>
            {
                Console.WriteLine($"StockReservedEvent after:{context.Saga}");
            }));
    }
}