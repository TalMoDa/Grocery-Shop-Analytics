using ShopAnalytics.Data.Entities.EF;

namespace ShopAnalytics.Data.Repositories.Interfaces;

public interface ISalesRepository : IBaseRepository<Sale>
{
    Task<decimal> GetSalesSumByDateRangeAsync(Guid shopId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<Sale>> GetSalesByDateRangeAsync(Guid shopId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}