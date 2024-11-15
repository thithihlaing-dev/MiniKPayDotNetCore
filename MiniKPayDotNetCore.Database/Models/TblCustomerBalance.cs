using System;
using System.Collections.Generic;

namespace MiniKPayDotNetCore.Database.Models;

public partial class TblCustomerBalance
{
    public int BalanceId { get; set; }

    public int CustomerId { get; set; }

    public decimal? Balance { get; set; }

    public string BalancePin { get; set; } = null!;
}
