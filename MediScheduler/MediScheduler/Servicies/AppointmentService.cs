using MediScheduler.DTO;
using MediScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AppointmentService
{
    private readonly MediSchedulerContext _context;

    public AppointmentService(MediSchedulerContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveAppointmentAsync(AppointmentDTO appointmentDTO)
    {
        var appointment = new Appointment
        {
            DoctorScheduleId = appointmentDTO.DoctorScheduleId,
            PatientId = appointmentDTO.PatientId,
            AppointmentStatusId = appointmentDTO.AppointmentStatusId
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<AppointmentDTO>> GetAppointmentsByPatientUserNameAsync(string userName)
    {
        var appointments = await _context.Appointments
            .Include(a => a.DoctorSchedule)
                .ThenInclude(ds => ds.Doctor)
            .Include(a => a.DoctorSchedule)
                .ThenInclude(ds => ds.Doctor.Specialty)
            .Include(a => a.AppointmentStatus)
            .Include(a => a.Patient)
            .Where(a => a.Patient.Users.Any(u => u.Username == userName))
            .Select(a => new AppointmentDTO
            {
                AppointmentId = a.AppointmentId,
                DoctorScheduleId = a.DoctorScheduleId,
                PatientId = a.PatientId,
                AppointmentStatusId = a.AppointmentStatusId,
                DoctorFirstName = a.DoctorSchedule.Doctor.FirstName,
                DoctorLastName = a.DoctorSchedule.Doctor.LastName,
                SpecialtyName = a.DoctorSchedule.Doctor.Specialty.SpecialtyName,
                Date = a.DoctorSchedule.Date,
                StartTime = a.DoctorSchedule.StartTime,
                EndTime = a.DoctorSchedule.EndTime,
                AppointmentStatusName = a.AppointmentStatus.AppointmentStatusName
            })
            .ToListAsync();

        return appointments;
    }
}
