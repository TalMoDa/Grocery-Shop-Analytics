using ShopAnalytics.Data.Entities.EF;
using ShopAnalytics.Data.Repositories.Interfaces;

namespace ShopAnalytics.Services.Interfaces;

public interface ISalesRepository : IBaseRepository<Sale>
{
    Task<decimal> GetSalesSumByDateRangeAsync(Guid shopId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}