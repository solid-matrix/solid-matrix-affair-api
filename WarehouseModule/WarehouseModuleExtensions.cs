using SolidMatrix.Affair.Api.Core;

namespace SolidMatrix.Affair.Api.WarehouseModule;

public static class WarehouseModuleExtensions
{
    public static IServiceCollection AddWarehouseModule(this IServiceCollection services)
    {
        services.AddDbContext<DbContextService>();
        services.AddScoped<CRUDService<DbContextService>>();
        return services;
    }
    public static IApplicationBuilder UseWarehouseModule(this IApplicationBuilder app)
    {
        return app;
    }
}
