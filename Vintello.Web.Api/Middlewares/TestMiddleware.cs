namespace Vintello.Web.Api.Middlewares;

public class TestMiddleware
{
    private readonly RequestDelegate _next;
    public TestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var testQuery = context.Request.Query["test"]; 
        if (!string.IsNullOrEmpty(testQuery))
        {
            Console.WriteLine(testQuery);
        }
        await _next(context);
    }
}

public static class TestMiddlewareExtension
{
    public static IApplicationBuilder UseTest(this IApplicationBuilder builder)
    { 
        builder.UseMiddleware<TestMiddleware>();
        return builder;
    }
}