using MediScheduler.Entities;
using MediScheduler.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SpecialtyService
{
    private readonly MediSchedulerContext _context;

    public SpecialtyService(MediSchedulerContext context)
    {
        _context = context;
    }

    public async Task<List<SpecialtyDTO>> GetAllSpecialtiesAsync()
    {
        var specialties = await _context.Specialties
            .Select(s => new SpecialtyDTO
            {
                SpecialtyId = s.SpecialtyId,
                SpecialtyName = s.SpecialtyName
            })
            .ToListAsync();

        return specialties;
    }
}
