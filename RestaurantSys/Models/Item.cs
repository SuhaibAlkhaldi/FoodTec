using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class Item
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public decimal? Price { get; set; }

    public string? Image { get; set; }

    public double? ItemRate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual ICollection<ItemOption> ItemOptions { get; set; } = new List<ItemOption>();

    public virtual ICollection<OfferItem> OfferItems { get; set; } = new List<OfferItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
