using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Test.Integration;

public class RoleApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly VintelloContext _context;

    public RoleApiTest(WebApplicationFactory<Program> factory)
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
        CreatedRolesDto role = new CreatedRolesDto { Name = "Дебил" };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Roles");
        request.Content = JsonContent.Create(role);

        var response = await _client.SendAsync(request);
        
        var result = await response.Content.ReadFromJsonAsync<RetrievedRoleDto>();
        response.EnsureSuccessStatusCode();
        Assert.Equal("Дебил", result!.Name);
    }
    
    [Theory]
    [InlineData("GET")]
    public async Task Get_ReturnOK(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Roles");

        var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var categories = (await response.Content.ReadFromJsonAsync<List<RetrievedRolesDto>>())!;
        Assert.IsType<List<RetrievedRolesDto>>(categories);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetById_ReturnOK(string method)
    {
        try
        {
            _context.Roles.Add(new Role { Id = 10000, Name = "Тест" });
            await _context.SaveChangesAsync();
        }
        catch 
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), "api/Roles/10000");

        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadFromJsonAsync<RetrievedRoleDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(10000, responseContent!.Id);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetById_ReturnNotFound(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "api/Roles/10000000");

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
            _context.Roles.Add(new Role { Id = id, Name = "Фигня", Description = "Обычная" });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), $"api/Roles/{id}");
        request.Content = JsonContent.Create(new UpdatedRoleDto { Name = "Фигня обновленная" });

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
            _context.Roles.Add(new Role
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

        var request = new HttpRequestMessage(new HttpMethod(method), $"api/Roles/{id}");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}