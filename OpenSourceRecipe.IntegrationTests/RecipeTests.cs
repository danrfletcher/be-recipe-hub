using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace OpenSourceRecipe.IntegrationTests;

public class UserEndpoints(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CreateRecipeAllParams_ShouldSucceed()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/create-recipe");
        var response = await _client.SendAsync(request);
    }

    [Fact]
    public async Task CreateRecipeMissingParams_ShouldFail()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/create-recipe")
    }
    
    [Fact]
    public async Task CreateRecipeNotLoggedIn_ShouldFail()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/create-recipe")
    }
}
