using MediatR;
using ShopAnalytics.Common.Models.ResultPattern;
using ShopAnalytics.Models;

namespace ShopAnalytics.Api.Shop.GetShops;

public record GetShopsQuery() : IRequest<Result<List<ShopDto>>>;