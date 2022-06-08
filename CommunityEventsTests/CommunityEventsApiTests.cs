using System.Net;
using System.Net.Http.Json;
using CommunityEventsApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CommunityEventsTests;

public class ApiTests
{
    [Fact]
    public async Task TestRootEndpoint()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetStringAsync("/");

        Assert.Equal("Community Events Api", response);
    }

    [Fact]
    public async Task ReturnOkWhenCorrectJsonIsSentToCreateEvent()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var result = await client.PostAsJsonAsync("/events", new Event
        {
            Title = "Test event",
            Date = "19/06/22",
            Time = "08:00",
            Location = "Santa Claus Village",
            Latitude = 66.543701M,
            Longitude = 25.844311M,
            Price = 0.00M,
            Description = "Have a magical time with Santa",
            Link = "test",
            ImageUrl = "test"
        });

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ReturnBadRequestWhenCreatingEventWithInvalidObject()
    {
        await using var application = new WebApplicationFactory<Program>();

        var client = application.CreateClient();

        var result = await client.PostAsJsonAsync("/events", new Event());

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        var validationResult = await result.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();
        Assert.NotNull(validationResult);
        Assert.Equal("The Title field is required.", validationResult!.Errors["Title"][0]);
    }
}
