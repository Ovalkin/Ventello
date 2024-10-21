using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Test.Integration.Helpers;

namespace Vintello.Test.Integration;

public class CategoryApiTest : IClassFixture<CustomWebApplicationFactory>
{
    public CategoryApiTest(CustomWebApplicationFactory factory)
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
    public async Task CreateCategory_WithCreatedCategoryDataIsValidAndRoleAdmin_ShouldReturnCreated(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.AdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        CreatedCategoryDto category = new CreatedCategoryDto { Name = "Тестовая категория" };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Categories");
        request.Content = JsonContent.Create(category);

        var response = await _client.SendAsync(request);

        var result = await response.Content.ReadFromJsonAsync<RetrievedCategoryDto>();
        response.EnsureSuccessStatusCode();
        Assert.Equal("Тестовая категория", result!.Name);
    }
    
    [Theory]
    [InlineData("POST")]
    public async Task CreateCategory_WithCreatedCategoryDataIsValidAndRoleSuperAdmin_ShouldReturnCreated(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.SuperAdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        CreatedCategoryDto category = new CreatedCategoryDto { Name = "Тестовая категория" };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Categories");
        request.Content = JsonContent.Create(category);

        var response = await _client.SendAsync(request);

        var result = await response.Content.ReadFromJsonAsync<RetrievedCategoryDto>();
        response.EnsureSuccessStatusCode();
        Assert.Equal("Тестовая категория", result!.Name);
    }
    
    [Theory]
    [InlineData("POST")]
    public async Task CreateCategory_WithCreatedCategoryDataIsValidAndRoleClint_ShouldReturnForbidden(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.ClientUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        CreatedCategoryDto category = new CreatedCategoryDto { Name = "Тестовая категория" };
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Categories");
        request.Content = JsonContent.Create(category);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Theory]
    [InlineData("GET")]
    public async Task RetrieveCategories_With_ShouldReturnCategories(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "/api/Categories");

        var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var categories = await response.Content.ReadFromJsonAsync<List<RetrievedCategoriesDto>>();
        Assert.IsType<List<RetrievedCategoriesDto>>(categories);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetCategory_WithCategoryIsExist_ShouldReturnOKCategory(string method)
    {
        const int id = 10;
        try
        {
            _context.Categories.Add(new Category { Id = id, Name = "Тест" });
            await _context.SaveChangesAsync();
        }
        catch
        {
            //
        }

        var request = new HttpRequestMessage(new HttpMethod(method), $"api/Categories/{id}");

        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadFromJsonAsync<RetrievedCategoriesDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(id, responseContent!.Id);
    }

    [Theory]
    [InlineData("GET")]
    public async Task GetCategory_WithCategoryIsNotExist_ShouldReturnNotFound(string method)
    {
        var request = new HttpRequestMessage(new HttpMethod(method), "api/Categories/10000000");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData("PUT")]
    public async Task UpdateCategory_WithCategoryDataIsValidAndRoleAdmin_ShouldReturnNoContent(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.AdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
    [InlineData("PUT")]
    public async Task UpdateCategory_WithCategoryDataIsValidAndRoleSuperAdmin_ShouldReturnNoContent(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.SuperAdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        const int id = 201;
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
    [InlineData("PUT")]
    public async Task UpdateCategory_WithCategoryDataIsValidAndRoleClient_ShouldReturnForbidden(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.ClientUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        const int id = 202;
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

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
    [Theory]
    [InlineData("PUT")]
    public async Task UpdateCategory_WithCategoryDataIsNotValidAndRoleAdmin_ShouldReturnForbidden(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.AdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        const int id = 203;
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
        request.Content = JsonContent.Create(new UpdatedCategoryDto { Name = "5432rkvsklfs!!Kl//" });

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("DELETE")]
    public async Task DeleteCategory_WithRoleSuperAdmin_ReturnNoContent(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.SuperAdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
    
    [Theory]
    [InlineData("DELETE")]
    public async Task DeleteCategory_WithRoleAdmin_ReturnForbidden(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.AdminUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
    
    [Theory]
    [InlineData("DELETE")]
    public async Task DeleteCategory_WithRoleClient_ReturnForbidden(string method)
    {
        var token = TestAuthHelper.GenerateToken(Users.ClientUser);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}