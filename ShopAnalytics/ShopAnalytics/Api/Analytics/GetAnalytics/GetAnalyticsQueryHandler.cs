using MediatR;
using ShopAnalytics.Common.Models.ResultPattern;
using ShopAnalytics.Models;
using ShopAnalytics.Services.Interfaces;

namespace ShopAnalytics.Api.Analytics.GetAnalytics;

public class GetAnalyticsQueryHandler(IAnalyticsService analyticsService) : IRequestHandler<GetAnalyticsQuery, Result<List<DayAnalyticsDto>>>
{
    public async Task<Result<List<DayAnalyticsDto>>> Handle(GetAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var analytics = await analyticsService.GetAnalyticsAsync(request.ShopId, request.FromDate, request.ToDate, cancellationToken);
        return analytics.ToList();
    }
}