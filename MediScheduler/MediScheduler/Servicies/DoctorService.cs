using MediScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediScheduler.DTO;
using MediScheduler.Components.Pages;

public class DoctorService
{
    private readonly MediSchedulerContext _context;

    public DoctorService(MediSchedulerContext context)
    {
        _context = context;
    }

    public async Task<List<DoctorDTO>> GetDoctorsAsync()
    {
        var doctorDetails = await (from doctor in _context.Doctors
                                   join specialty in _context.Specialties
                                   on doctor.SpecialtyId equals specialty.SpecialtyId
                                   join contactData in _context.ContactData
                                   on doctor.ContactDataId equals contactData.ContactDataId into contactDataGroup
                                   from contactData in contactDataGroup.DefaultIfEmpty()
                                   select new DoctorDTO
                                   {
                                       DoctorId = doctor.DoctorId,
                                       FirstName = doctor.FirstName,
                                       LastName = doctor.LastName,
                                       LicenseNo = doctor.LicenseNo,
                                       SpecialtyId = specialty.SpecialtyId,
                                       SpecialtyName = specialty.SpecialtyName,
                                       Address = contactData.Adress,
                                       Phone = contactData.Phone,
                                       Email = contactData.Email
                                   }).ToListAsync();

        return doctorDetails;
    }


    public async Task AddDoctorAsync(DoctorDTO doctorDTO)
    {
        if (doctorDTO != null)
        {
            var doctor = new MediScheduler.Entities.Doctor
            {
                FirstName = doctorDTO.FirstName,
                LastName = doctorDTO.LastName,
                LicenseNo = doctorDTO.LicenseNo,
                SpecialtyId = doctorDTO.SpecialtyId,
                ContactData = new ContactData
                {
                    Adress = doctorDTO.Address,
                    Phone = doctorDTO.Phone,
                    Email = doctorDTO.Email
                }
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

        }
    }

    public async Task UpdateDoctorAsync(DoctorDTO doctorDto)
    {
        var doctor = await _context.Doctors
            .Include(d => d.ContactData)
            .FirstOrDefaultAsync(d => d.DoctorId == doctorDto.DoctorId);

        if (doctor == null)
        {
            throw new KeyNotFoundException("Doctor not found");
        }

        doctor.FirstName = doctorDto.FirstName;
        doctor.LastName = doctorDto.LastName;
        doctor.LicenseNo = doctorDto.LicenseNo;
        doctor.SpecialtyId = doctorDto.SpecialtyId;

        if (doctor.ContactData == null)
        {
            doctor.ContactData = new ContactData
            {
                Adress = doctorDto.Address,
                Phone = doctorDto.Phone,
                Email = doctorDto.Email
            };
        }
        else
        {
            doctor.ContactData.Adress = doctorDto.Address;
            doctor.ContactData.Phone = doctorDto.Phone;
            doctor.ContactData.Email = doctorDto.Email;
        }

        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<DoctorDTO> GetDoctorByIdAsync(int doctorId)
    {
        var doctor = await _context.Doctors
            .Include(d => d.ContactData)
            .Include(d => d.Specialty)
            .FirstOrDefaultAsync(d => d.DoctorId == doctorId);

        if (doctor == null)
        {
            throw new KeyNotFoundException("Doctor not found");
        }

        return new DoctorDTO
        {
            DoctorId = doctor.DoctorId,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            LicenseNo = doctor.LicenseNo,
            SpecialtyId = doctor.Specialty.SpecialtyId,
            SpecialtyName = doctor.Specialty.SpecialtyName,
            Address = doctor.ContactData.Adress,
            Phone = doctor.ContactData.Phone,
            Email = doctor.ContactData.Email
        };
    }
}

