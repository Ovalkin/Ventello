using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Vintello.Common.EntityModel.PostgreSql;

public static class VintelloContextExtensions
{
    public static IServiceCollection AddVintelloContext(this IServiceCollection services, string connection = "..")
    {
        string connectionString = "host=localhost; port=5432; database=vintello;  username=postgres;  password=7878;";
        services.AddDbContext<VintelloContext>(options =>
            options.UseNpgsql(connectionString)
        );
        return services;
    }
}