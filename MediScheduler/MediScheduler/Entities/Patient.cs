using System;
using System.Collections.Generic;

namespace MediScheduler.Entities;

public partial class Patient
{
    public int PatientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? ContactDataId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ContactData? ContactData { get; set; }

    public virtual ICollection<RecordHistory> RecordHistories { get; set; } = new List<RecordHistory>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
