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
        var today = DateOnly.FromDateTime(DateTime.Today);

        var schedules = await _context.DoctorSchedules
            .Include(ds => ds.Doctor)
            .Include(ds => ds.DoctorScheduleStatus)
            .Include(ds => ds.Doctor.Specialty)
            .Where(ds => ds.Date >= today && (ds.DoctorScheduleStatusId == 1 || ds.DoctorScheduleStatusId == 3)) // Filter for dates from today onwards and statuses special (3) or available (1)
            .Select(ds => new DoctorScheduleDTO
            {
                DoctorScheduleId = ds.DoctorScheduleId,
                DoctorId = ds.DoctorId,
                DoctorFirstName = ds.Doctor.FirstName,
                DoctorLastName = ds.Doctor.LastName,
                SpecialtyId = ds.Doctor.SpecialtyId,
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

    public async Task<DoctorScheduleDTO> GetScheduleByIdAsync(int doctorScheduleId)
    {
        var schedule = await _context.DoctorSchedules
            .Include(ds => ds.Doctor)
            .Include(ds => ds.DoctorScheduleStatus)
            .Include(ds => ds.Doctor.Specialty)
            .Where(ds => ds.DoctorScheduleId == doctorScheduleId)
            .Select(ds => new DoctorScheduleDTO
            {
                DoctorScheduleId = ds.DoctorScheduleId,
                DoctorId = ds.DoctorId,
                DoctorFirstName = ds.Doctor.FirstName,
                DoctorLastName = ds.Doctor.LastName,
                SpecialtyId = ds.Doctor.SpecialtyId,
                SpecialtyName = ds.Doctor.Specialty != null ? ds.Doctor.Specialty.SpecialtyName : string.Empty,
                Date = ds.Date,
                StartTime = ds.StartTime,
                EndTime = ds.EndTime,
                DoctorScheduleStatusId = ds.DoctorScheduleStatusId,
                DoctorScheduleStatusName = ds.DoctorScheduleStatus != null ? ds.DoctorScheduleStatus.DoctorScheduleStatusName : string.Empty
            })
            .FirstOrDefaultAsync();

        return schedule;
    }

    public async Task UpdateDoctorScheduleStatusAsync(int doctorScheduleId, int statusId)
    {
        var schedule = await _context.DoctorSchedules.FirstOrDefaultAsync(ds => ds.DoctorScheduleId == doctorScheduleId);
        if (schedule != null)
        {
            schedule.DoctorScheduleStatusId = statusId;
            await _context.SaveChangesAsync();
        }
    }
}
