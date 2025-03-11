using ShopAnalytics.Models;

namespace ShopAnalytics.Mappers.Shop;

public static class ShopMapper
{
     public static ShopDto MapToDto(this Data.Entities.EF.Shop shop)
     {
         return new ShopDto
         {
             Id = shop.Id,
             Name = shop.Name
         };
     }
     
    public static Data.Entities.EF.Shop MapToEntity(this ShopDto shopDto)
    {
        return new Data.Entities.EF.Shop
        {
            Name = shopDto.Name
        };
    }
}