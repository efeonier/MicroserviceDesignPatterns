using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Payment.API.Repositories.Interface;
using Shared;

namespace Payment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    public PaymentController(IPublishEndpoint publishEndpoint, IPaymentRepository paymentRepository)
    {
        _publishEndpoint = publishEndpoint;
        _paymentRepository = paymentRepository;
    }
}