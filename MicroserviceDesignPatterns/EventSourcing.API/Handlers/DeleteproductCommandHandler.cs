using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;
using MediatR;

namespace EventSourcing.API.Handlers;

public class DeleteproductCommandHandler(ProductStream productStream) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        productStream.Deleted(request.Id);
        await productStream.SaveAsync();
    }
}