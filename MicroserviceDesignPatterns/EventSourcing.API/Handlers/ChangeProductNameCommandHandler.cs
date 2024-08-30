using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;
using MediatR;

namespace EventSourcing.API.Handlers;

public class ChangeProductNameCommandHandler(ProductStream productStream) : IRequestHandler<ChangeProductNameCommand>
{
    public async Task Handle(ChangeProductNameCommand request, CancellationToken cancellationToken)
    {
        productStream.NameChanged(request.ChangeProductNameDto);
        await productStream.SaveAsync();
    }
}