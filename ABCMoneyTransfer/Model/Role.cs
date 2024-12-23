using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ABCMoneyTransfer.Model;

public partial class Role : IdentityRole<int>
{

    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
