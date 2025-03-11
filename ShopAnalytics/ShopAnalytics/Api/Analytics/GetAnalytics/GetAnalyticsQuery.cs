using MediatR;
using ShopAnalytics.Common.Models.ResultPattern;
using ShopAnalytics.Models;

namespace ShopAnalytics.Api.Analytics.GetAnalytics;


public record GetAnalyticsQuery(Guid ShopId, DateTime FromDate, DateTime ToDate) : IRequest<Result<List<DayAnalyticsDto>>>;