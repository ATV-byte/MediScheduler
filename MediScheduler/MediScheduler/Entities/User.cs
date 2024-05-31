using System;
using System.Collections.Generic;

namespace MediScheduler.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int? PatientId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual Patient? Patient { get; set; }
}
