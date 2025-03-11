using ShopAnalytics.Models;

namespace ShopAnalytics.Services.Interfaces;

public interface IAnalyticsService 
{
    Task<IReadOnlyList<DayAnalyticsDto>> GetAnalyticsAsync(Guid shopId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    
}

