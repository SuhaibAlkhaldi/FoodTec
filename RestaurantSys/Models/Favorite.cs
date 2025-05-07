using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class Favorite
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ItemId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Item? Item { get; set; }

    public virtual User? User { get; set; }
}
