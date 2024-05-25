using System;
using System.Collections.Generic;

namespace MediScheduler.Entities;

public partial class Specialty
{
    public int SpecialtyId { get; set; }

    public string SpecialtyName { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
