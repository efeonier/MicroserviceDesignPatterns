using MassTransit;
using Shared.SagaOrchestration.Concrete;
using Shared.SagaOrchestration.Interface;

namespace SagaStateMachineWorkerService.Entities;

public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
{
    public Event<IOrderCreatedRequestEvent> OrderCreatedRequestEvent { get; set; }
    public State OrderCreated { get; private set; }
    public OrderStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderCreatedRequestEvent,
        y => y.CorrelateBy<int>(x => x.OrderId,
        z => z.Message.OrderId).SelectId(_ => Guid.NewGuid()));


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
                Console.WriteLine($"OrderCreatedRequestEvent after:{context.Saga.ToString()}");
            }));
    }
}