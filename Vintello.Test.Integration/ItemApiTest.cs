using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Test.Integration;

public class ItemApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly VintelloContext _context;

    public ItemApiTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        _context = serviceProvider.GetRequiredService<VintelloContext>();
    }

    [Theory]
    [InlineData("POST")]
    public async Task Post_ReturnCreated(string method)
    {
        CreatedItemDto user = new CreatedItemDto
            { UserId = 1, CategoryId = 1, Title = "Тестовый айтем", Price = 1000, Images = ["путь"] };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Items");
        request.Content = JsonContent.Create(user);

        var response = await _client.SendAsync(request);

        var result = await response.Content.ReadFromJsonAsync<RetrievedItemDto>();
        response.EnsureSuccessStatusCode();
        Assert.Equal("Хуйня", result!.Title);
    }

    [Theory]
    [InlineData("GET")]
    public async Task Get_ReturnOK(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Items");

        var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var users = (await response.Content.ReadFromJsonAsync<List<RetrievedItemDto>>())!;
        Assert.IsType<List<RetrievedItemDto>>(users);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetById_ReturnOK(string method)
    {
        const int id = 10000;
        try
        {
            _context.Items.Add(new Item
                { Id = id, UserId = 1, CategoryId = 1, Title = "Тестовый айтем", Images = ["путь"], Price = 1000 });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), "api/Item/10000");

        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadFromJsonAsync<RetrievedItemDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(10000, responseContent!.Id);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetById_ReturnNotFound(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "api/Items/10000000");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData("PUT")]
    public async Task Update_ReturnNoContent(string method)
    {
        const int id = 200;
        try
        {
            _context.Items.Add(new Item
            {
                Id = id, UserId = 1, CategoryId = 1, Status = "created", Title = "Тестовый айтем", Images = ["путь"],
                Price = 1000,
                CreatedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), $"/api/Items/{id}");
        request.Content = JsonContent.Create(new UpdatedItemDto { Title = "Тестовый измененный айтем" });

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Theory]
    [InlineData("DELETE")]
    public async Task Delete_ReturnNoContent(string method)
    {
        const int id = 300;
        try
        {
            _context.Items.Add(new Item
            {
                Id = id, UserId = 1, CategoryId = 1, Status = "created", Title = "Тестовый айтем", Images = ["путь"],
                Price = 1000,
                CreatedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), $"api/Items/{id}");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}