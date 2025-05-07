using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class ItemOption
{
    public int ItemOptionId { get; set; }

    public string? ItemOptionArabicName { get; set; }

    public string? ItemOptionEnglishName { get; set; }

    public bool? IsRequired { get; set; }

    public int ItemId { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual Item Item { get; set; } = null!;
}
