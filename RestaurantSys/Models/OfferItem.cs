using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class OfferItem
{
    public int Id { get; set; }

    public int? OfferId { get; set; }

    public int? ItemId { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Offer? Offer { get; set; }
}
