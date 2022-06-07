namespace CommunityEventsApi.Data;

public class EventsDatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;

    public string DatabaseName { get; set; } = string.Empty;

    public string EventsCollectionName { get; set; } = string.Empty;
}
