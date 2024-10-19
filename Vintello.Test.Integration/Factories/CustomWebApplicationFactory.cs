using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Test.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<VintelloContext>));

            if (descriptor != null) services.Remove(descriptor);
            services.AddDbContext<VintelloContext>(options =>
            {
                var sp = services.BuildServiceProvider();
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("TestConnection");
                options.UseNpgsql(connectionString);
            });
        });
    }
}