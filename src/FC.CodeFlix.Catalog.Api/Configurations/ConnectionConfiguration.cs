using FC.CodeFlix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.Api.Configurations;

public static class ConnectionConfiguration
{
    public static IServiceCollection AddAppConnections(this IServiceCollection services)
    {
        services.AddDbConnection();

        return services;
    }

    private static IServiceCollection AddDbConnection(this IServiceCollection services)
    {
        services.AddDbContext<CodeflixCatalogDbContext>(options => options.UseInMemoryDatabase("InMemory-DSV-Database"));

        return services;
    }
}
