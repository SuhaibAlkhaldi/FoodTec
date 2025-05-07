using System;
using System.Collections.Generic;

namespace RestaurantSys.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? AssignedUserAmount { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
