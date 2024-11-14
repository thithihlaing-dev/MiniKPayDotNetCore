using System;
using System.Collections.Generic;

namespace MiniKPayDotNetCore.Database.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string FromMobile { get; set; } = null!;

    public string ToMobile { get; set; } = null!;

    public decimal TransactionAmount { get; set; }

    public string TransactionMessage { get; set; } = null!;

    public string TransactionPin { get; set; } = null!;

    public DateTime DateTime { get; set; }
}
