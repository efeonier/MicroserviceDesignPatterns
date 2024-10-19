using EventSourcing.API.DTOs;
using EventSourcing.Shared.Events.Concretes;
using EventSourcing.Shared.Events.Interfaces;
using EventStore.Client;
using System.Text.Json;

namespace EventSourcing.API.EventStores;

public class ProductStream(EventStoreClient eventStoreClient)
{
    public static string StreamName => "ProductStream";
    public static string GroupName => "agroup";
    private readonly LinkedList<IEvent> _events = [];

    public void Created(CreateProductDto createProductDto)
    {
        _events.AddLast(new ProductCreatedEvent
        {
            Id = Guid.NewGuid(),
            Name = createProductDto.Name,
            Price = createProductDto.Price,
            Stock = createProductDto.Stock,
            UserId = createProductDto.UserId
        });
    }

    public void NameChanged(ChangeProductNameDto changeProductNameDto)
    {
        _events.AddLast(new ProductNameChangedEvent { ChangedName = changeProductNameDto.Name, Id = changeProductNameDto.Id });
    }

    public void PriceChanged(ChangeProductPriceDto changeProductPriceDto)
    {
        _events.AddLast(new ProductPriceChangedEvent() { ChangedPrice = changeProductPriceDto.Price, Id = changeProductPriceDto.Id });
    }

    public void Deleted(Guid id)
    {
        _events.AddLast(new ProductDeletedEvent { Id = id });
    }
    public async Task SaveAsync()
    {
        var newEvents = _events.AsEnumerable().Select(s => new EventData(
        Uuid.NewUuid(),
        s.GetType().Name,
        JsonSerializer.SerializeToUtf8Bytes(s),
        JsonSerializer.SerializeToUtf8Bytes(s.GetType().FullName)));
        await eventStoreClient.AppendToStreamAsync(StreamName, StreamState.Any, newEvents);
        _events.Clear();
    }
}