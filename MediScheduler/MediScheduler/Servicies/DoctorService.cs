using MediScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DoctorService
{
    private readonly MediSchedulerContext _context;

    public DoctorService(MediSchedulerContext context)
    {
        _context = context;
    }

    public async Task<List<Doctor>> GetDoctorsAsync()
    {
        return await _context.Doctors.Include(d => d.Specialty).ToListAsync();
    }

    public async Task<List<Specialty>> GetSpecialtiesAsync()
    {
        return await _context.Specialties.ToListAsync();
    }

    public async Task AddDoctorAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
    }
}
