namespace MediScheduler.DTO
{
    public class DoctorDTO
    {
            public int DoctorId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string LicenseNo { get; set; }
            public int SpecialtyId { get; set; }
            public string SpecialtyName { get; set; }
            public string? Address { get; set; }
            public string? Phone { get; set; }
            public string? Email { get; set; }

    }
}
