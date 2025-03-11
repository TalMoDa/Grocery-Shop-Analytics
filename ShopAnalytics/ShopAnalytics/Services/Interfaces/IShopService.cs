using ShopAnalytics.Models;

namespace ShopAnalytics.Services.Interfaces;

public interface IShopService
{
    Task<IReadOnlyList<ShopDto>> GetShopsAsync(CancellationToken cancellationToken = default);
}