using System;

namespace MediScheduler.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }

        public int? DoctorScheduleId { get; set; }

        public int? PatientId { get; set; }

        public int? AppointmentStatusId { get; set; }

        public string DoctorFirstName { get; set; }

        public string DoctorLastName { get; set; }

        public string SpecialtyName { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string AppointmentStatusName { get; set; }
    }
}
