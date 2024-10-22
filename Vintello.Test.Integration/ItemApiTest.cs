using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Test.Integration.Helpers;

namespace Vintello.Test.Integration;

public class ItemApiTest : IClassFixture<CustomWebApplicationFactory>
{
    public ItemApiTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        _context = serviceProvider.GetRequiredService<VintelloContext>();

        try
        {
            _context.Categories.Add(new Category
            {
                Id = 1,
                Name = "Категория для тестов"
            });
        }
        catch
        {
            //
        }
    }
    
    private readonly HttpClient _client;
    private readonly VintelloContext _context;

    [Theory]
    [InlineData("POST")]
    public async Task CreateItem_WithCreatedItemDataIsValidAndRoleClient_ShouldReturnCreated(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.ClientUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        try
        {
            _context.Users.Add(new User
            { 
                Id = Users.ClientUser.Id,
                Role = RolesEnum.Client,
                FirstName = "Имя",
                Email = "test4@example.com", 
                Password = "пароль" });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }
        
        CreatedItemDto item = new CreatedItemDto 
            {
                UserId = 3,
                CategoryId = 1, 
                Title = "Тестовый айтем",
                Price = 1000.00m,
                Images = ["путь"] 
            };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Items");
        request.Content = JsonContent.Create(item);

        var response = await _client.SendAsync(request);

        var result = await response.Content.ReadFromJsonAsync<RetrievedItemDto>();
        response.EnsureSuccessStatusCode();
        Assert.Equal("Тестовый айтем", result!.Title);
    }
    
    [Theory]
    [InlineData("POST")]
    public async Task CreateItem_WithCreatedItemDataIsValidAndRoleAdmin_ShouldReturnCreated(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.AdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        try
        {
            _context.Users.Add(new User
                { 
                    Id = Users.AdminUser.Id,
                    Role = RolesEnum.Admin,
                    FirstName = "Имя",
                    Email = "test3@example.com", 
                    Password = "пароль" });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }
        
        CreatedItemDto item = new CreatedItemDto 
            {
                UserId = 1,
                CategoryId = 1, 
                Title = "Тестовый айтем",
                Price = 1000.00m,
                Images = ["путь"] 
            };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Items");
        request.Content = JsonContent.Create(item);

        var response = await _client.SendAsync(request);

        var result = await response.Content.ReadFromJsonAsync<RetrievedItemDto>();
        response.EnsureSuccessStatusCode();
        Assert.Equal("Тестовый айтем", result!.Title);
    }
    
    [Theory]
    [InlineData("POST")]
    public async Task CreateItem_WithCreatedItemDataIsValidAndRoleSuperAdmin_ShouldReturnCreated(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.SuperAdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        try
        {
            _context.Users.Add(new User
                { 
                    Id = Users.SuperAdminUser.Id,
                    Role = RolesEnum.SuperAdmin,
                    FirstName = "Имя",
                    Email = "test3@example.com", 
                    Password = "пароль" });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }
        
        CreatedItemDto item = new CreatedItemDto 
            {
                UserId = 3,
                CategoryId = 1, 
                Title = "Тестовый айтем",
                Price = 1000.00m,
                Images = ["путь"] 
            };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Items");
        request.Content = JsonContent.Create(item);

        var response = await _client.SendAsync(request);

        var result = await response.Content.ReadFromJsonAsync<RetrievedItemDto>();
        response.EnsureSuccessStatusCode();
        Assert.Equal("Тестовый айтем", result!.Title);
    }
    
    [Theory]
    [InlineData("POST")]
    public async Task CreateItem_WithCreatedItemDataIsNotValidAndRoleClient_ShouldReturnBadRequest(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.ClientUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        try
        {
            _context.Users.Add(new User
                { 
                    Id = Users.ClientUser.Id,
                    Role = RolesEnum.SuperAdmin,
                    FirstName = "Имя",
                    Email = "test3@example.com", 
                    Password = "пароль" });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }
        
        CreatedItemDto item = new CreatedItemDto 
            {
                UserId = 2,
                CategoryId = 1, 
                Title = "Тестовый айтем",
                Price = 1000.00m,
                Images = ["путь"] 
            };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Items");
        request.Content = JsonContent.Create(item);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("GET")]
    public async Task RetrieveItems_WithAll_ReturnOK(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Items");

        var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var users = (await response.Content.ReadFromJsonAsync<List<RetrievedItemDto>>())!;
        Assert.IsType<List<RetrievedItemDto>>(users);
    }

    [Theory]
    [InlineData("GET")]
    public async Task RetrieveItem_WithItemIsExist_ReturnOK(string method)
    {
        const int id = 10000;
        try
        {
            _context.Items.Add(new Item
            {
                Id = id,
                UserId = 1,
                CategoryId = 1,
                Title = "Тестовый айтем",
                Status = "create",
                CreatedAt = DateTime.Now,
                Images = ["путь"],
                Price = 1000.00m
            });
            _context.Users.Add(Users.AdminUser);
            _context.Users.Add(Users.ClientUser);
            _context.Users.Add(Users.SuperAdminUser);
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), $"api/Items/{id}");

        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadFromJsonAsync<RetrievedItemDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(10000, responseContent!.Id);
    }

    [Theory]
    [InlineData("GET")]
    public async Task RetrieveItem_WithItemIsNotExist_ReturnNotFound(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "api/Items/10000000");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData("PUT")]
    public async Task UpdateItem_WithRoleIsAdmin_ReturnNoContent(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.AdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        
        const int id = 201;
        try
        {
            _context.Items.Add(new Item
            {
                Id = id,
                UserId = Users.AdminUser.Id,
                CategoryId = 1,
                Title = "Тестовый айтем",
                Status = "create",
                CreatedAt = DateTime.Now,
                Images = ["путь"],
                Price = 1000.00m
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
    [InlineData("PUT")]
    public async Task UpdateItem_WithRoleIsSuperAdmin_ReturnNoContent(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.SuperAdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        
        const int id = 202;
        try
        {
            _context.Items.Add(new Item
            {
                Id = id,
                UserId = Users.SuperAdminUser.Id,
                CategoryId = 1,
                Title = "Тестовый айтем",
                Status = "create",
                CreatedAt = DateTime.Now,
                Images = ["путь"],
                Price = 1000.00m
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
    [InlineData("PUT")]
    public async Task UpdateItem_WithRoleIsClient_ReturnNoContent(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.ClientUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        
        const int id = 203;
        try
        {
            _context.Items.Add(new Item
            {
                Id = id,
                UserId = Users.ClientUser.Id,
                CategoryId = 1,
                Title = "Тестовый айтем",
                Status = "create",
                CreatedAt = DateTime.Now,
                Images = ["путь"],
                Price = 1000.00m
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
    [InlineData("PUT")]
    public async Task UpdateItem_WithRoleIsClient_ReturnBadRequest(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.ClientUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        const int id = 204;
        try
        {
            _context.Items.Add(new Item
            {
                Id = id,
                UserId = Users.ClientUser.Id,
                CategoryId = 1,
                Title = "Тестовый айтем",
                Status = "create",
                CreatedAt = DateTime.Now,
                Images = ["путь"],
                Price = 1000.00m
            });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), $"/api/Items/{id}");
        request.Content = JsonContent.Create(new UpdatedItemDto { Title = "Тестовый измененный", UserId = "10"});

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}