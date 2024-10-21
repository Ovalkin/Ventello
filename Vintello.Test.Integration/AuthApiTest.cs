using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Test.Integration;

public class AuthApiTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly VintelloContext _context;

    public AuthApiTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        _context = serviceProvider.GetRequiredService<VintelloContext>();
    }

    [Theory]
    [InlineData("POST")]
    public async Task Login_WithLoginAndPasswordIsValid_ShouldReturnJwtToken(string method)
    {
        const int id = 1;
        const string login = "test@example.com";
        const string password = "1234qwer";
        try
        {
            _context.Users.Add(new User
            {
                Id = id,
                Role = RolesEnum.Client,
                FirstName = "Тест",
                LastName = "Тестов",
                Email = login,
                Phone = "89991112233",
                Password = password
            });
            await _context.SaveChangesAsync();
        }
        catch
        {
            /**/
        }

        var request = new HttpRequestMessage(new HttpMethod(method), "api/Auth/login");
        var content = new LoginDto { Email = login, Password = password };
        request.Content = JsonContent.Create(content);

        var response = await _client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        Assert.IsType<string>(result);
    }

    [Theory]
    [InlineData("POST")]
    public async Task Login_WithLoginAndPasswordIsNotValid_ShouldReturnBadRequest(string method)
    {
        const int id = 1;
        const string login = "test@example.com";
        const string password = "1234qwer";
        try
        {
            _context.Users.Add(new User
            {
                Id = id,
                Role = RolesEnum.Client,
                FirstName = "Тест",
                LastName = "Тестов",
                Email = login,
                Phone = "89991112233",
                Password = password
            });
            await _context.SaveChangesAsync();
        }
        catch
        {
            /**/
        }

        var request = new HttpRequestMessage(new HttpMethod(method), "api/Auth/login");
        var content = new LoginDto { Email = login };
        request.Content = JsonContent.Create(content);

        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}