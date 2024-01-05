using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using OpenSourceRecipes.Models;
using Xunit;
using Xunit.Abstractions;

namespace OpenSourceRecipe.IntegrationTests;
public class IngredientEndpointTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CreateIngredientEndpoint_ShouldSucceed()
    {
       //Arrange
       var newIngredient = new
       {
            IngredientName = "Test Ingredient",
            Nutrition = "Carbs, fats, protein"
       };

       var request = new HttpRequestMessage(HttpMethod.Post, "api/ingredients")
       {
            Content = new StringContent(JsonConvert.SerializeObject(newIngredient), Encoding.UTF8, "application/json")
       };

       //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

       //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async Task CreateIngredientEndpointNoParams_ShouldFail()
    {
        //Act
        var request = new HttpRequestMessage(HttpMethod.Post, "api/ingredients")
        {
            Content = new StringContent("", Encoding.UTF8, "application/json")
        };
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}   