using System;
using System.Collections.Generic;

namespace MiniKPayDotNetCore.Database.Models;

public partial class CustomerBalance
{
    public int BalanceId { get; set; }

    public string CustomerId { get; set; } = null!;

    public decimal? Balance { get; set; }

    public string BalancePin { get; set; } = null!;
}
