using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Test.Integration;

public class CategoryApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public CategoryApiTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("GET")]
    public async Task Get_ReturnOK(string method)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<VintelloContext>();
            context.Categories.Add(new Category { Name = "Test Category", Description = "A test category." });
            await context.SaveChangesAsync();
        }

        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Categories");

        var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        List<Category> categories = (await response.Content.ReadFromJsonAsync<List<Category>>())!;
        Assert.NotEmpty(categories);
        Assert.Equal("Test Category", categories.First().Name);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetById_ReturnNotFound(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), $"api/Categories/1000000");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}