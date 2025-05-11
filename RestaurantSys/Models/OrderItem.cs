using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ItemId { get; set; }

    public int Quantity { get; set; }

    public decimal ItemPrice { get; set; }
    public int UserID { get; set; }

    public string? Notes { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Order? Order { get; set; }

    public User User { get; set; }
}
