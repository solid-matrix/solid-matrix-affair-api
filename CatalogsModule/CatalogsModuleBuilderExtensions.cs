namespace SolidMatrix.Affair.Api.CatalogsModule;

public static class CatalogsModuleBuilderExtensions
{
    public static IServiceCollection AddCatalogsModule(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException("builder");
        }
        services.AddHostedService<CatalogsHostService>();
        services.AddSingleton<CatalogsService>();
        return services;
    }
}
