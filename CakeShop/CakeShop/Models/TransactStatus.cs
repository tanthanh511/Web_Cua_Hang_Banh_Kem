using System;
using System.Collections.Generic;

namespace CakeShop.Models;

public partial class TransactStatus
{
    public int TranSactStatusId { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
