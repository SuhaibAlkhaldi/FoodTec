using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class OfferCategory
{
    public int Id { get; set; }

    public int? OfferId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Offer? Offer { get; set; }
}
