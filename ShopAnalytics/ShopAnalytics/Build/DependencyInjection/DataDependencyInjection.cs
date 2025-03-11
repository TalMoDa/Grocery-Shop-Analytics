using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopAnalytics.Data;
using ShopAnalytics.Data.Repositories.Implementations;
using ShopAnalytics.Data.Repositories.Interfaces;
using ShopAnalytics.Settings;

namespace ShopAnalytics.Build.DependencyInjection;

public static class DataDependencyInjection
{
    public static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        var connectionString = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionStrings>>().Value.DefaultConnection;
        
        services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));
        services.AddDbContext<GroceryShopContext>(options => options.UseNpgsql(connectionString));
        services.AddRepositories();
        return services;
    }


    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IShopRepository, ShopRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<ISalesRepository, SalesRepository>();
        return services;
    }
}