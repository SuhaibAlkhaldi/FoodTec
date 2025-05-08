using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class Chat
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ClientId { get; set; }

    public int? DriverId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? Timestamp { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? Client { get; set; }

    public virtual User? Driver { get; set; }

    public virtual Order? Order { get; set; }
}
