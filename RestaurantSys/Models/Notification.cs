using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Type { get; set; }

    public bool? IsRead { get; set; }

    public int? OrderId { get; set; }

    public int? OfferId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Offer? Offer { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User? User { get; set; }
}
