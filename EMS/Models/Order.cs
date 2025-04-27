using System;
using System.Collections.Generic;

namespace EMS.Models;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public string OrderDate { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
