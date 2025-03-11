using FluentAssertions;
using NSubstitute;
using ShopAnalytics.Data.Entities.EF;
using ShopAnalytics.Data.Repositories.Interfaces;
using ShopAnalytics.Models;
using ShopAnalytics.Services.Implementations;
using ShopAnalytics.Services.Interfaces;
using Xunit;

namespace ShopAnalyticsTests.ServicesTests;

public class AnalyticsServiceTests
{
    private readonly IExpenseRepository _expenseRepository = Substitute.For<IExpenseRepository>();
    private readonly ISalesRepository _salesRepository = Substitute.For<ISalesRepository>();
    private readonly IAnalyticsService _testUnit;

    public AnalyticsServiceTests()
    {
        _testUnit = new AnalyticsService(_expenseRepository, _salesRepository);
    }

    [Fact]
    public async Task GetAnalyticsAsync_ShouldReturnCorrectAnalytics()
    {
        // Arrange
        var shopId = Guid.NewGuid();
        var fromDate = new DateTime(2024, 3, 1);
        var toDate = new DateTime(2024, 3, 3);
        var cancellationToken = CancellationToken.None;

        var sales = new List<Sale>
        {
            new() { Date = new DateTime(2024, 3, 1), Amount = 200 },
            new() { Date = new DateTime(2024, 3, 1), Amount = 300 },
            new() { Date = new DateTime(2024, 3, 2), Amount = 150 }
        };

        var expenses = new List<Expense>
        {
            new() { Date = new DateTime(2024, 3, 1), Amount = 100 },
            new() { Date = new DateTime(2024, 3, 3), Amount = 50 }
        };

        _salesRepository.GetSalesByDateRangeAsync(shopId, fromDate, toDate, cancellationToken)
            .Returns(Task.FromResult<IReadOnlyList<Sale>>(sales));

        _expenseRepository.GetExpensesByDateRangeAsync(shopId, fromDate, toDate, cancellationToken)
            .Returns(Task.FromResult<IReadOnlyList<Expense>>(expenses));

        // Act
        var result = await _testUnit.GetAnalyticsAsync(shopId, fromDate, toDate, cancellationToken);

        // Assert
        result.Should().HaveCount(3);

        result.Should().ContainEquivalentOf(new DayAnalyticsDto
        {
            Date = new DateTime(2024, 3, 1),
            Income = 500,  // 200 + 300
            Outcome = 100, // 100
            Revenue = 400   // 500 - 100
        });

        result.Should().ContainEquivalentOf(new DayAnalyticsDto
        {
            Date = new DateTime(2024, 3, 2),
            Income = 150,  // 150
            Outcome = 0,   // No expenses
            Revenue = 150  // 150 - 0
        });

        result.Should().ContainEquivalentOf(new DayAnalyticsDto
        {
            Date = new DateTime(2024, 3, 3),
            Income = 0,    // No sales
            Outcome = 50,  // 50
            Revenue = -50  // 0 - 50
        });
    }
}