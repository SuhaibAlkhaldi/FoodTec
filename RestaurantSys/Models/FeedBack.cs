using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class FeedBack
{
    public int FeedBackId { get; set; }

    public double? Rate { get; set; }

    public string Comment { get; set; } = null!;

    public int UserId { get; set; }

    public int ItemId { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
