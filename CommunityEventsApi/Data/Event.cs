using MongoDB.Bson.Serialization.Attributes;

namespace CommunityEventsApi.Data;

public class Event
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

    public string Id { get; set; }

    public string Title { get; set; }

    public string Date { get; set; }

    public string Time { get; set; }

    public string Location { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public string Link { get; set; } = null;

    public string ImageUrl { get; set; } = null;
}
