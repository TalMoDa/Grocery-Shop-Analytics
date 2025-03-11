using System;
using System.Collections.Generic;

namespace ShopAnalytics.Data.Entities.EF;

public partial class Expense
{
    public Guid Id { get; set; }

    public Guid ShopId { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public virtual Shop Shop { get; set; } = null!;
}