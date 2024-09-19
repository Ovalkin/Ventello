using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Vintello.Common.EntityModel.PostgreSql;

public static class VintelloContextExtensions
{
    /// <summary>
    /// Adds VintelloContext to the specified IServiceCollection. Uses the Sqlite database provider.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connection">Добавляет другую строку подключения к базе данных</param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
    public static IServiceCollection AddVintelloContext(this IServiceCollection services, string connection = "..")
    {
        string connectionString = "host=localhost; port=5432; database=vintello;  username=postgres;  password=7878;";
        services.AddDbContext<VintelloContext>(options =>
            options.UseNpgsql(connectionString)
        );
        return services;
    }
}