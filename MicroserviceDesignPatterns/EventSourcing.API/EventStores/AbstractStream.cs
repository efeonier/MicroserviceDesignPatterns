using EventSourcing.Shared.Events.Interfaces;
using EventStore.Client;
using System.Text.Json;

namespace EventSourcing.API.EventStores;

public abstract class AbstractStream(string streamName, EventStoreClient client)
{
    protected readonly LinkedList<IEvent> Events = [];
    private string StreamName { get; } = streamName;

    public async Task SaveAsync()
    {
        var newEvents = Events.AsEnumerable().Select(s => new EventData(
        Uuid.NewUuid(),
        s.GetType().Name,
        JsonSerializer.SerializeToUtf8Bytes(s),
        JsonSerializer.SerializeToUtf8Bytes(s.GetType().FullName)));
        await client.AppendToStreamAsync(StreamName, StreamState.Any, newEvents);
        Events.Clear();
    }
}