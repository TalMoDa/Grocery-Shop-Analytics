using ShopAnalytics.Data.Entities.EF;

namespace ShopAnalytics.Data.Repositories.Interfaces;

public interface IExpenseRepository  : IBaseRepository<Expense>
{
    Task<decimal> GetExpensesSumByDateRangeAsync(Guid shopId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<Expense>> GetExpensesByDateRangeAsync(Guid shopId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    
}