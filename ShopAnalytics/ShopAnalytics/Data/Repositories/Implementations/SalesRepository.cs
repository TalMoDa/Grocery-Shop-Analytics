using Dapper;
using ShopAnalytics.Data.Entities.EF;
using ShopAnalytics.Data.Repositories.Interfaces;

namespace ShopAnalytics.Data.Repositories.Implementations;

public class SalesRepository(GroceryShopContext context) : BaseRepository<Sale>(context), ISalesRepository
{

    public async Task<decimal> GetSalesSumByDateRangeAsync(Guid shopId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        using var connection = await CreateDapperConnectionAsync(cancellationToken);
        
        const string sql = @"
            SELECT COALESCE(SUM(amount), 0)
            FROM sales
            WHERE Shop_Id = @ShopId
              AND Date >= @StartDate
              AND Date <= @EndDate";

        var parameters = new
        {
            ShopId = shopId,
            StartDate = startDate,
            EndDate = endDate
        };

        return await connection.ExecuteScalarAsync<decimal>(new CommandDefinition(sql, parameters, cancellationToken: cancellationToken));
    }
    public async Task<IReadOnlyList<Sale>> GetSalesByDateRangeAsync(Guid shopId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        using var connection = await CreateDapperConnectionAsync(cancellationToken);
        
        const string sql = @"
            SELECT Id, Shop_Id, Amount, Date
            FROM sales
            WHERE Shop_Id = @ShopId
              AND Date >= @StartDate
              AND Date <= @EndDate
            ORDER BY Date";

        var parameters = new
        {
            ShopId = shopId,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await connection.QueryAsync<Sale>(new CommandDefinition(sql, parameters, cancellationToken: cancellationToken));
        
        return result.AsList();
    }
}