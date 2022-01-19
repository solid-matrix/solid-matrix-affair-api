namespace SolidMatrix.Affair.Api.WarehouseModule;

public static class WarehouseModuleBuilderExtensions
{
    public static IServiceCollection AddWarehouseModule(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException("builder");
        }
        return services;
    }
}
