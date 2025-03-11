using ShopAnalytics.Data.Entities.EF;

namespace ShopAnalytics.Data.Repositories.Interfaces;

public interface IShopRepository : IBaseRepository<Shop>
{
    Task<List<Shop>> GetShopsAsync(CancellationToken cancellationToken = default);
}