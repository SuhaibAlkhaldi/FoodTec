using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class Offer
{
    public int Id { get; set; }

    public string? TitleEn { get; set; }

    public string? TitleAr { get; set; }

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? LimitAmount { get; set; }

    public string? Code { get; set; }

    public string? Image { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<OfferCategory> OfferCategories { get; set; } = new List<OfferCategory>();

    public virtual ICollection<OfferItem> OfferItems { get; set; } = new List<OfferItem>();
}
