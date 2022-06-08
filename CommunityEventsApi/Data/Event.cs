using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CommunityEventsApi.Data;

public class Event
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [Required, MinLength(2)]
    public string Title { get; set; } = string.Empty;

    public string Date { get; set; } = string.Empty;

    public string Time { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    [Range(-90, 90)]
    public decimal Latitude { get; set; }

    [Range(-180, 180)]
    public decimal Longitude { get; set; }

    public decimal Price { get; set; }

    [Required, MinLength(2)]
    public string Description { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;
}
