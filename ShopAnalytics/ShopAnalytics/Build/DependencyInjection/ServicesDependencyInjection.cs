using ShopAnalytics.Services.Implementations;
using ShopAnalytics.Services.Interfaces;

namespace ShopAnalytics.Build.DependencyInjection;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IShopService, ShopService>();
        services.AddScoped<IAnalyticsService , AnalyticsService>();
        return services;
    }
}