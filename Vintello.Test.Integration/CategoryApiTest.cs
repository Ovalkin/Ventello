using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Test.Integration;

public class CategoryApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly VintelloContext _context;

    public CategoryApiTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        _context = serviceProvider.GetRequiredService<VintelloContext>();
    }

    [Theory]
    [InlineData("POST")]
    public async Task Create_ReturnCreated(string method)
    {
        CreatedCategoryDto category = new CreatedCategoryDto { Name = "Тестовая категория" };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Categories");
        request.Content = JsonContent.Create(category);

        var response = await _client.SendAsync(request);

        var result = await response.Content.ReadFromJsonAsync<RetrievedCategoryDto>();
        response.EnsureSuccessStatusCode();
        Assert.Equal("Тестовая категория", result!.Name);
    }

    [Theory]
    [InlineData("GET")]
    public async Task Get_ReturnOK(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Categories");

        var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var categories = (await response.Content.ReadFromJsonAsync<List<RetrievedCategoriesDto>>())!;
        Assert.IsType<List<RetrievedCategoriesDto>>(categories);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetById_ReturnOK(string method)
    {
        try
        {
            _context.Categories.Add(new Category { Id = 10000, Name = "Тест" });
            await _context.SaveChangesAsync();
        }
        catch 
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), "api/Categories/10000");

        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadFromJsonAsync<RetrievedCategoriesDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(10000, responseContent!.Id);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetById_ReturnNotFound(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "api/Categories/10000000");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData("PUT")]
    public async Task Put_ReturnNoContent(string method)
    {
        const int id = 200;
        try
        {
            _context.Categories.Add(new Category { Id = id, Name = "Фигня", Description = "Обычная" });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), $"api/Categories/{id}");
        request.Content = JsonContent.Create(new UpdatedCategoryDto { Name = "Фигня обновленная" });

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
            _context.Categories.Add(new Category
            {
                Id = id,
                Name = "Удаляемая категория",
                Description = "Не должна оставаться в бд"
            });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), $"api/Categories/{id}");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}