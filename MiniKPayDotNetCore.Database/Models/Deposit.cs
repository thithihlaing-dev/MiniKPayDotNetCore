using System;
using System.Collections.Generic;

namespace MiniKPayDotNetCore.Database.Models;

public partial class Deposit
{
    public int DepositId { get; set; }

    public string DepositMobileNo { get; set; } = null!;

    public decimal DepositAmount { get; set; }

    public DateTime DateTime { get; set; }
}
