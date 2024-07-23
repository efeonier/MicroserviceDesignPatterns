using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.API.DTOs;
using Order.API.Entities;
using Order.API.Enums;
using Order.API.Repositories.Interface;
using Shared;
using Shared.SagaOrchestration.Concrete;
using Shared.SagaOrchestration.Interface;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    public OrderController(IOrderRepository orderRepository, ISendEndpointProvider sendEndpointProvider)
    {
        _orderRepository = orderRepository;
        _sendEndpointProvider = sendEndpointProvider;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(OrderCreateDto orderCreate)
    {
        try
        {
            var newOrder = new Entities.Order()
            {
                BuyerId = orderCreate.BuyerId,
                Status = OrderStatus.Suspend,
                Address = orderCreate.Address.Adapt<Address>(),
                CreatedDate = DateTime.Now,
                OrderItems = orderCreate.OrderItems.Adapt<List<OrderItem>>()
            };

            await _orderRepository.AddAsync(newOrder);

            var payments = orderCreate.Payment.Adapt<PaymentMessage>();
            payments.TotalPrice = orderCreate.OrderItems.Sum(s => s.Price * s.Count);


            var orderCreatedRequestEvent = new OrderCreatedRequestEvent() { BuyerId = orderCreate.BuyerId, OrderId = newOrder.Id, Payment = payments, OrderItems = orderCreate.OrderItems.Adapt<List<OrderItemMessage>>() };
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqSettings.OrderSaga}"));
            await sendEndpoint.Send<IOrderCreatedRequestEvent>(orderCreatedRequestEvent);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.ToString().PadLeft(250));
        }
        return Ok();
    }
}