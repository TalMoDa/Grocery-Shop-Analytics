using System;
using System.Collections.Generic;

namespace ShopAnalytics.Data.Entities.EF;

public partial class Shop
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}