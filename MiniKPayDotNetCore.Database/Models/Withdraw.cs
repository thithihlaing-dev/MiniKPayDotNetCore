using System;
using System.Collections.Generic;

namespace MiniKPayDotNetCore.Database.Models;

public partial class Withdraw
{
    public int WithdrawId { get; set; }

    public string WithdrawMobileNo { get; set; } = null!;

    public decimal WithdrawAmount { get; set; }

    public string? WithdrawPin { get; set; }

    public DateTime DateTime { get; set; }
}
