using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.API.DTOs;
using Order.API.Entities;
using Order.API.Enums;
using Order.API.Repositories.Interface;
using Shared;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    public OrderController(IPublishEndpoint publishEndpoint, IOrderRepository orderRepository)
    {
        _publishEndpoint = publishEndpoint;
        _orderRepository = orderRepository;
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

            var orderCreatedEvent = new OrderCreatedEvent() { BuyerId = orderCreate.BuyerId, OrderId = newOrder.Id, PaymentMessage = payments, OrderItems = orderCreate.OrderItems.Adapt<List<OrderItemMessage>>() };
            await _publishEndpoint.Publish(orderCreatedEvent);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.ToString().PadLeft(250));
        }
        return Ok();
    }
}