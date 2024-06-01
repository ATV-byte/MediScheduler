using MediScheduler.Entities;
using MediScheduler.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DoctorScheduleService
{
    private readonly MediSchedulerContext _context;

    public DoctorScheduleService(MediSchedulerContext context)
    {
        _context = context;
    }

    public async Task<List<DoctorScheduleDTO>> GetSchedulesAsync()
    {
        var schedules = await _context.DoctorSchedules
            .Include(ds => ds.Doctor)
            .Include(ds => ds.DoctorScheduleStatus)
            .Include(ds => ds.Doctor.Specialty)
            .Select(ds => new DoctorScheduleDTO
            {
                DoctorScheduleId = ds.DoctorScheduleId,
                DoctorId = ds.DoctorId,
                DoctorFirstName = ds.Doctor.FirstName,
                DoctorLastName = ds.Doctor.LastName,
                SpecialtyId = ds.Doctor.SpecialtyId, // Added SpecialtyId
                SpecialtyName = ds.Doctor.Specialty != null ? ds.Doctor.Specialty.SpecialtyName : string.Empty,
                Date = ds.Date,
                StartTime = ds.StartTime,
                EndTime = ds.EndTime,
                DoctorScheduleStatusId = ds.DoctorScheduleStatusId,
                DoctorScheduleStatusName = ds.DoctorScheduleStatus != null ? ds.DoctorScheduleStatus.DoctorScheduleStatusName : string.Empty
            })
            .ToListAsync();

        return schedules;
    }
}
