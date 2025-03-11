using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using ShopAnalytics.Data.Entities.EF;
using ShopAnalytics.Data.Repositories.Interfaces;
using ShopAnalytics.Mappers.Shop;
using ShopAnalytics.Models;
using ShopAnalytics.Services.Implementations;
using ShopAnalytics.Services.Interfaces;
using Xunit;

public class ShopServiceTests
{
    private readonly IShopRepository _shopRepository = Substitute.For<IShopRepository>();
    private readonly IShopService _shopService;

    public ShopServiceTests()
    {
        _shopService = new ShopService(_shopRepository);
    }

    [Fact]
    public async Task GetShopsAsync_ShouldReturnMappedShops()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        
        var shopEntities = new List<Shop>
        {
            new() { Id = Guid.NewGuid(), Name = "Shop 1" },
            new() { Id = Guid.NewGuid(), Name = "Shop 2"}
        };

        _shopRepository.GetShopsAsync(cancellationToken)
            .Returns(Task.FromResult(shopEntities));

        // Act
        var result = await _shopService.GetShopsAsync(cancellationToken);

        // Assert
        result.Should().HaveCount(2);
        result.Should().ContainEquivalentOf(shopEntities[0].MapToDto());
        result.Should().ContainEquivalentOf(shopEntities[1].MapToDto());

        // Verify repository method was called once
        await _shopRepository.Received(1).GetShopsAsync(cancellationToken);
    }
}