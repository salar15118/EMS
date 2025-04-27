using System;
using System.Collections.Generic;

namespace EMS.Models;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string CustomerAddress { get; set; } = null!;

    public decimal CustomerContact { get; set; }

    public string? OrderId { get; set; }

    public virtual Order? Order { get; set; }
}
