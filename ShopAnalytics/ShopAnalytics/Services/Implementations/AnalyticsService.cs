using ShopAnalytics.Data.Repositories.Interfaces;
using ShopAnalytics.Models;
using ShopAnalytics.Services.Interfaces;
using ShopAnalytics.Data.Entities.EF;
using ShopAnalytics.Helpers;

namespace ShopAnalytics.Services.Implementations;

public class AnalyticsService(IExpenseRepository expenseRepository, ISalesRepository salesRepository) : IAnalyticsService
{
    public async Task<IReadOnlyList<DayAnalyticsDto>> GetAnalyticsAsync(Guid shopId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        var normalizedFromDate = fromDate.Date;
        var normalizedToDate = toDate.Date;

        var (sales, expenses) = await TaskHelper.ExecuteTasksAsync(
            salesRepository.GetSalesByDateRangeAsync(shopId, normalizedFromDate, normalizedToDate, cancellationToken),
            expenseRepository.GetExpensesByDateRangeAsync(shopId, normalizedFromDate, normalizedToDate, cancellationToken),
            "Failed to retrieve analytics data"
        );

        var salesByDate = GroupSalesByDate(sales);
        var expensesByDate = GroupExpensesByDate(expenses);
        
        return GenerateDailyAnalytics(normalizedFromDate, normalizedToDate, salesByDate, expensesByDate);
    }
    
    
    private static Dictionary<DateTime, decimal> GroupSalesByDate(IEnumerable<Sale> sales)
    {
        return sales
            .GroupBy(s => s.Date.Date)
            .ToDictionary(g => g.Key, g => g.Sum(s => s.Amount));
    }

    private static Dictionary<DateTime, decimal> GroupExpensesByDate(IEnumerable<Expense> expenses)
    {
        return expenses
            .GroupBy(e => e.Date.Date)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
    }

    private static List<DayAnalyticsDto> GenerateDailyAnalytics(
        DateTime fromDate, 
        DateTime toDate, 
        Dictionary<DateTime, decimal> salesByDate, 
        Dictionary<DateTime, decimal> expensesByDate)
    {
        var result = new List<DayAnalyticsDto>();

        for (var date = fromDate; date <= toDate; date = date.AddDays(1))
        {
            salesByDate.TryGetValue(date, out var salesSum);
            expensesByDate.TryGetValue(date, out var expensesSum);

            result.Add(new DayAnalyticsDto
            {
                Date = date,
                Income = salesSum,
                Outcome = expensesSum,
                Revenue = salesSum - expensesSum
            });
        }

        return result;
    }
}

