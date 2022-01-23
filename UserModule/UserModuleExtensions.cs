namespace SolidMatrix.Affair.Api.UserModule;

public static class UserModuleExtensions
{
    public static IServiceCollection AddUserModule(this IServiceCollection services)
    {
        return services;
    }
    public static IApplicationBuilder UseUserModule(this IApplicationBuilder app)
    {
        return app;
    }
}
