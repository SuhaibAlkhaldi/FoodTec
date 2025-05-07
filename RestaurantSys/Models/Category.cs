using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? Image { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<OfferCategory> OfferCategories { get; set; } = new List<OfferCategory>();
}
