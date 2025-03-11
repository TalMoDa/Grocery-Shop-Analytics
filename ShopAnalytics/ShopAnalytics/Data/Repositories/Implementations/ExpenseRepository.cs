using Dapper;
using Microsoft.EntityFrameworkCore;
using ShopAnalytics.Data.Entities.EF;
using ShopAnalytics.Data.Repositories.Interfaces;

namespace ShopAnalytics.Data.Repositories.Implementations;

public class ExpenseRepository(GroceryShopContext context) : BaseRepository<Expense>(context), IExpenseRepository
{
    public async Task<decimal> GetExpensesSumByDateRangeAsync(Guid shopId, DateTime startDate, DateTime endDate, 
        CancellationToken cancellationToken = default)
    {
        using var connection = await CreateDapperConnectionAsync(cancellationToken);
        
        const string sql = @"
            SELECT COALESCE(SUM(Amount), 0)
            FROM expenses
            WHERE Shop_Id = @ShopId
              AND Date>= @StartDate
              AND Date <= @EndDate";

        var parameters = new
        {
            ShopId = shopId,
            StartDate = startDate,
            EndDate = endDate
        };

        return await connection.ExecuteScalarAsync<decimal>(
            new CommandDefinition(sql, parameters, cancellationToken: cancellationToken));
    }

    public async Task<IReadOnlyList<Expense>> GetExpensesByDateRangeAsync(Guid shopId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        using var connection = await CreateDapperConnectionAsync(cancellationToken);
        
        const string sql = @"
            SELECT Id, Shop_Id, Date, Amount
            FROM expenses
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

        var result = await connection.QueryAsync<Expense>(
            new CommandDefinition(sql, parameters, cancellationToken: cancellationToken));
            
        return result.AsList();
    }
}
