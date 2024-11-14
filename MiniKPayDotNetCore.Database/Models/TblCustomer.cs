using System;
using System.Collections.Generic;

namespace MiniKPayDotNetCore.Database.Models;

public partial class TblCustomer
{
    public int CustomerId { get; set; }

    public string CustomerFullName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string CustomerPin { get; set; } = null!;
}
