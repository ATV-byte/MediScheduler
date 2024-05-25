using System;
using System.Collections.Generic;

namespace MediScheduler.Entities;

public partial class RecordHistory
{
    public int RecordHistoryId { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int? AppointmentId { get; set; }

    public string? Notes { get; set; }

    public string? Image { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
