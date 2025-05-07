using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? AddressId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public int? AssignedDriverId { get; set; }

    public int? FeedbackId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Address? Address { get; set; }

    public virtual User? AssignedDriver { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual FeedBack? Feedback { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Payment? Payment { get; set; }

    public virtual User? User { get; set; }
}
