namespace SolidMatrix.Affair.Api.CatalogsModule;

public static class CatalogsModuleExtensions
{
    public static IServiceCollection AddCatalogsModule(this IServiceCollection services)
    {
        services.AddSingleton<CatalogsService>();
        return services;
    }

    public static IApplicationBuilder UseCatalogsModule(this IApplicationBuilder app)
    {
        app.ApplicationServices.GetRequiredService<CatalogsService>().Initialize();
        return app;
    }
}
