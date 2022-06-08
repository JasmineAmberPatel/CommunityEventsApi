using CommunityEventsApi.Data;
using MiniValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<EventsDatabaseSettings>(builder.Configuration.GetSection("EventsDatabaseSettings"));
builder.Services.AddSingleton<EventsService>();
var app = builder.Build();

app.MapGet("/", () => "Community Events Api");

app.MapGet("/events", async (EventsService eventsService) => await eventsService.Get());

app.MapGet("/events/{id}", async (EventsService eventsService, string id) =>
{
    var communityEvent = await eventsService.Get(id);
    return communityEvent is null ? Results.NotFound() : Results.Ok(communityEvent);
});

app.MapPost("/events", async (EventsService eventsService, Event newEvent) =>
{
    if (MiniValidator.TryValidate(newEvent, out var errors))
    {
        await eventsService.Create(newEvent);
        return Results.Ok();
    }
    return Results.ValidationProblem(errors);
});

app.MapPut("/events/{id}", async (EventsService eventsService, string id, Event updatedEvent) =>
{
    var communityEvent = await eventsService.Get(id);
    if (communityEvent is null) return Results.NotFound();

    updatedEvent.Id = communityEvent.Id;
    await eventsService.Update(id, updatedEvent);

    return Results.NoContent();
});

app.MapDelete("/events/{id}", async (EventsService eventsService, string id) =>
{
    var communityEvent = await eventsService.Get(id);
    if (communityEvent is null) return Results.NotFound();

    await eventsService.Remove(communityEvent.Id);

    return Results.NoContent();
});

app.Run();

public partial class Program { }