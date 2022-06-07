using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CommunityEventsApi.Data;

public class EventsService
{
    private readonly IMongoCollection<Event> _events;

    public EventsService(IOptions<EventsDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);

        _events = mongoClient.GetDatabase(options.Value.DatabaseName)
            .GetCollection<Event>(options.Value.EventsCollectionName);
    }

    public async Task<List<Event>> Get() =>
        await _events.Find(_ => true).ToListAsync();

    public async Task<Event> Get(string id) =>
        await _events.Find(e => e.Id == id).FirstOrDefaultAsync();

    public async Task Create(Event newEvent) =>
        await _events.InsertOneAsync(newEvent);

    public async Task Update(string id, Event updateEvent) =>
        await _events.ReplaceOneAsync(e => e.Id == id, updateEvent);

    public async Task Remove(string id) =>
        await _events.DeleteOneAsync(e => e.Id == id);
}

