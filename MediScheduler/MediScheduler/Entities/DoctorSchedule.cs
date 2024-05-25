using System;
using System.Collections.Generic;

namespace MediScheduler.Entities;

public partial class DoctorSchedule
{
    public int DoctorScheduleId { get; set; }

    public int? DoctorId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int? DoctorScheduleStatusId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Doctor? Doctor { get; set; }

    public virtual DoctorScheduleStatus? DoctorScheduleStatus { get; set; }
}
