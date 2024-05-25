using System;
using System.Collections.Generic;

namespace MediScheduler.Entities;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? ContactDataId { get; set; }

    public int? SpecialtyId { get; set; }

    public string LicenseNo { get; set; } = null!;

    public virtual ContactData? ContactData { get; set; }

    public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();

    public virtual ICollection<RecordHistory> RecordHistories { get; set; } = new List<RecordHistory>();

    public virtual Specialty? Specialty { get; set; }
}
