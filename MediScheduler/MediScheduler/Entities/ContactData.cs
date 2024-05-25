using System;
using System.Collections.Generic;

namespace MediScheduler.Entities;

public partial class ContactData
{
    public int ContactDataId { get; set; }

    public string? Adress { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
