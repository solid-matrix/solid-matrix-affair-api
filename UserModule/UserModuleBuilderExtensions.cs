namespace SolidMatrix.Affair.Api.UserModule;

public static class UserModuleBuilderExtensions
{
    public static IServiceCollection AddUserModule(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException("builder");
        }
        return services;
    }
}
