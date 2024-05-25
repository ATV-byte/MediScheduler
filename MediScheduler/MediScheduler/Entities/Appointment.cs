using System;
using System.Collections.Generic;

namespace MediScheduler.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? DoctorScheduleId { get; set; }

    public int? PatientId { get; set; }

    public int? AppointmentStatusId { get; set; }

    public virtual AppointmentStatus? AppointmentStatus { get; set; }

    public virtual DoctorSchedule? DoctorSchedule { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<RecordHistory> RecordHistories { get; set; } = new List<RecordHistory>();
}
