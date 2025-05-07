using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class User
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ProfileImage { get; set; }

    public DateTime? JoinDate { get; set; }

    public bool? IsVerified { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? OtpCode { get; set; }

    public DateTime? OtpExpiry { get; set; }

    public DateOnly? Birthdate { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Chat> ChatClients { get; set; } = new List<Chat>();

    public virtual ICollection<Chat> ChatDrivers { get; set; } = new List<Chat>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> OrderAssignedDrivers { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderUsers { get; set; } = new List<Order>();

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Ticket> TicketClients { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketResolvedByAdmins { get; set; } = new List<Ticket>();
}
