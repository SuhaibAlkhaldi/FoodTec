using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class PaymentMethod
{
    public int PaymentMethodId { get; set; }

    public int UserId { get; set; }

    public string CardType { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public DateOnly ExpiryDate { get; set; }

    public string Cvc { get; set; } = null!;

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
