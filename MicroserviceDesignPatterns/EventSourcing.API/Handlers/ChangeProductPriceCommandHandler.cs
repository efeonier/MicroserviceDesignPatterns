using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;
using MediatR;

namespace EventSourcing.API.Handlers;

public class ChangeProductPriceCommandHandler(ProductStream productStream) : IRequestHandler<ChangeProductPriceCommand>
{
    public async Task Handle(ChangeProductPriceCommand request, CancellationToken cancellationToken)
    {
        productStream.PriceChanged(request.ChangeProductPriceDto);
        await productStream.SaveAsync();
    }
}