using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;


namespace ShopAnalytics.Data.Repositories.Implementations;

public abstract class BaseDapperRepository(GroceryShopContext context)
{
    private readonly string _connectionString = context.Database.GetConnectionString() ?? throw new ArgumentNullException(nameof(context));

    protected async Task<IDbConnection> CreateDapperConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}
