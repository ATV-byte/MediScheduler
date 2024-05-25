using System;
using System.Collections.Generic;

namespace MediScheduler.Entities;

public partial class AppointmentStatus
{
    public int AppointmentStatusId { get; set; }

    public string AppointmentStatusName { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
