using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopAnalytics.Common.Models.ResultPattern;
using ShopAnalytics.Data;
using ShopAnalytics.Data.Entities.EF;
using ShopAnalytics.Models;
using ShopAnalytics.Services.Interfaces;

namespace ShopAnalytics.Api.Shop.GetShops;

public class GetShopsQueryHandler(IShopService shopService,GroceryShopContext context) : IRequestHandler<GetShopsQuery, Result<List<ShopDto>>>
{
    public async Task<Result<List<ShopDto>>> Handle(GetShopsQuery request, CancellationToken cancellationToken)
    {
        var shops = await shopService.GetShopsAsync(cancellationToken);
        return shops.ToList();
    }
    
}