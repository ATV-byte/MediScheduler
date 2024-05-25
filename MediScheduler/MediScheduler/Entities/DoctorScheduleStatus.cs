using System;
using System.Collections.Generic;

namespace MediScheduler.Entities;

public partial class DoctorScheduleStatus
{
    public int DoctorScheduleStatusId { get; set; }

    public string DoctorScheduleStatusName { get; set; } = null!;

    public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();
}
