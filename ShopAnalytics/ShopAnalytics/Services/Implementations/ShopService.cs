using ShopAnalytics.Data.Repositories.Interfaces;
using ShopAnalytics.Mappers.Shop;
using ShopAnalytics.Models;
using ShopAnalytics.Services.Interfaces;

namespace ShopAnalytics.Services.Implementations;

public class ShopService(IShopRepository shopRepository) : IShopService
{
    public async Task<IReadOnlyList<ShopDto>> GetShopsAsync(CancellationToken cancellationToken = default)
    {
        var shopsEntities = await shopRepository.GetShopsAsync(cancellationToken);
        return shopsEntities.Select(shopEntity => shopEntity.MapToDto()).ToList();
    }
}