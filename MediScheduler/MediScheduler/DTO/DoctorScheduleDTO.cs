
namespace MediScheduler.DTO
{
    public class DoctorScheduleDTO
    {
        public int DoctorScheduleId { get; set; }

        public int? DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public int? SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public int? DoctorScheduleStatusId { get; set; }
        public string DoctorScheduleStatusName { get; set; } = null!;
    }
}
