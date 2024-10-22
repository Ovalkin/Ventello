using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Test.Integration;

public class UserApiTest : IClassFixture<CustomWebApplicationFactory>
{
    public UserApiTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        _context = serviceProvider.GetRequiredService<VintelloContext>();
    }


    private readonly HttpClient _client;
    private readonly VintelloContext _context;
   
    [Theory]
    [InlineData("POST")]
    public async Task Post_ReturnCreated(string method)
    {
        CreatedUserDto user = new CreatedUserDto
            { FirstName = "Имя", Email = "test2@example.com", Password = "пароль" };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Users");
        request.Content = JsonContent.Create(user);

        var response = await _client.SendAsync(request);

        var result = await response.Content.ReadFromJsonAsync<RetrievedUserDto>();
        response.EnsureSuccessStatusCode();
        Assert.Equal("Имя", result!.FirstName);
    }

    [Theory]
    [InlineData("GET")]
    public async Task Get_ReturnOK(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Users");

        var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var users = (await response.Content.ReadFromJsonAsync<List<RetrievedUsersDto>>())!;
        Assert.IsType<List<RetrievedUsersDto>>(users);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetById_ReturnOK(string method)
    {
        try
        {
            _context.Users.Add(new User
                { Id = 10000, FirstName = "Имя", Email = "test@example.com", Password = "пароль" });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), "api/Users/10000");

        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadFromJsonAsync<RetrievedUserDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(10000, responseContent!.Id);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetById_ReturnNotFound(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "api/Users/10000000");

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
            _context.Users.Add(
                new User { Id = id, FirstName = "Имя", Email = "test@example.com", Password = "пароль" });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), $"/api/Users/{id}");
        request.Content = JsonContent.Create(new UpdatedUserDto {LastName = "Фамилия" });

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
            _context.Users.Add(
                new User { Id = id, FirstName = "Имя", Email = "test@example.com", Password = "пароль" });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), $"api/Users/{id}");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}