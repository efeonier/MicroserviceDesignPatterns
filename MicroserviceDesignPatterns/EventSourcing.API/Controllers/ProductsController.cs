using EventSourcing.API.Commands;
using EventSourcing.API.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto createProductDto)
    {
        await mediator.Send(new CreateProductCommand() { CreateProductDto = createProductDto });
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> ChangeName(ChangeProductNameDto changeProductNameDto)
    {
        await mediator.Send(new ChangeProductNameCommand() { ChangeProductNameDto = changeProductNameDto });
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> ChangePrice(ChangeProductPriceDto changeProductPriceDto)
    {
        await mediator.Send(new ChangeProductPriceCommand() { ChangeProductPriceDto = changeProductPriceDto });
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteProductCommand() { Id = id });
        return NoContent();
    }
}