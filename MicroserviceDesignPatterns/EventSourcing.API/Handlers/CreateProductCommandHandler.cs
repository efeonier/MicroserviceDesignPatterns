using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;
using MediatR;

namespace EventSourcing.API.Handlers;

public class CreateProductCommandHandler(ProductStream productStream) : IRequestHandler<CreateProductCommand>
{
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        productStream.Created(request.CreateProductDto);
        await productStream.SaveAsync();
    }
}