using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Vintello.Common.EntityModel.PostgreSql;

public static class VintelloContextExtensions
{
    public static IServiceCollection AddVintelloContext(this IServiceCollection services, string connection)
    {
        services.AddDbContext<VintelloContext>(options =>
            {
                options.UseNpgsql(connection);
            });
        return services;
    }
}