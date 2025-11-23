using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using PolicyNotesService;
using PolicyNotesService.Models;

namespace PolicyNotesService.Tests.Integration;

public class PolicyNotesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PolicyNotesIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task POST_Notes_ShouldReturn201()
    {
        var note = new PolicyNote
        {
            PolicyNumber = "P555",
            Note = "This is a test"
        };

        var response = await _client.PostAsJsonAsync("/notes", note);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GET_Notes_ShouldReturn200()
    {
        var response = await _client.GetAsync("/notes");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_Notes_ById_ShouldReturn200_WhenFound()
    {
        var newNote = new PolicyNote { PolicyNumber = "PN1", Note = "Test Note" };

        var createdResponse = await _client.PostAsJsonAsync("/notes", newNote);
        var created = await createdResponse.Content.ReadFromJsonAsync<PolicyNote>();

        var response = await _client.GetAsync($"/notes/{created!.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
