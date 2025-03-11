using Dapper;
using Microsoft.EntityFrameworkCore;
using ShopAnalytics.Data.Entities.EF;
using ShopAnalytics.Data.Repositories.Interfaces;

namespace ShopAnalytics.Data.Repositories.Implementations;

public class ShopRepository(GroceryShopContext context) : BaseRepository<Shop>(context), IShopRepository
{
    public async Task<List<Shop>> GetShopsAsync(CancellationToken cancellationToken = default)
    { 
        using var connection = await CreateDapperConnectionAsync(cancellationToken);
        
        const string sql = @"
            SELECT Id, Name, Created
            FROM shops
            ORDER BY Name";
        
        var res = await connection.QueryAsync<Shop>(new CommandDefinition(sql, cancellationToken: cancellationToken));
        
        return res.ToList();
    }
}