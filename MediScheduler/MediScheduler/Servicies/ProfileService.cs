using System.Threading.Tasks;
using MediScheduler.DTO;
using MediScheduler.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediScheduler.Services
{
    public class ProfileService
    {
        private readonly MediSchedulerContext _appDbContext;

        public ProfileService(MediSchedulerContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> UpdateProfile(UserDTO userDTO, PatientDTO patientDTO)
        {
            var user = await _appDbContext.Users
                .Include(u => u.Patient)
                .ThenInclude(p => p.ContactData)
                .FirstOrDefaultAsync(u => u.Username == userDTO.UserName);

            if (user == null)
                return false;

            // Hash the password here before storing it
            user.Password = userDTO.Password;

            if (user.Patient != null)
            {
                user.Patient.FirstName = patientDTO.FirstName;
                user.Patient.LastName = patientDTO.LastName;
                user.Patient.ContactDataId = patientDTO.ContactDataId;

                if (user.Patient.ContactData != null)
                {
                    user.Patient.ContactData.Adress = patientDTO.Address;
                    user.Patient.ContactData.Phone = patientDTO.Phone;
                    user.Patient.ContactData.Email = patientDTO.Email;
                    _appDbContext.ContactData.Update(user.Patient.ContactData);
                }
                else if (patientDTO.ContactDataId.HasValue)
                {
                    var contactData = await _appDbContext.ContactData
                        .FirstOrDefaultAsync(cd => cd.ContactDataId == patientDTO.ContactDataId);

                    if (contactData != null)
                    {
                        contactData.Adress = patientDTO.Address;
                        contactData.Phone = patientDTO.Phone;
                        contactData.Email = patientDTO.Email;
                        user.Patient.ContactData = contactData;
                        _appDbContext.ContactData.Update(contactData);
                    }
                }
            }

            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<(UserDTO, PatientDTO)> GetProfileData(string userName)
        {
            var user = await _appDbContext.Users
                .Include(u => u.Patient)
                .ThenInclude(p => p.ContactData)
                .FirstOrDefaultAsync(u => u.Username == userName);

            if (user == null)
                return (null, null);

            var userDTO = new UserDTO
            {
                UserName = user.Username,
                Password = user.Password
            };

            var patientDTO = new PatientDTO
            {
                PatientId = user.Patient?.PatientId ?? 0,
                FirstName = user.Patient?.FirstName ?? string.Empty,
                LastName = user.Patient?.LastName ?? string.Empty,
                ContactDataId = user.Patient?.ContactDataId,
                Address = user.Patient?.ContactData?.Adress,
                Phone = user.Patient?.ContactData?.Phone,
                Email = user.Patient?.ContactData?.Email
            };

            return (userDTO, patientDTO);
        }

        public async Task<int> GetRisk(string userName)
        {
            if (userName == "tudorachevlad")
            {
                await Task.Delay(10);
                return 90;
            }
            else
            {
                await Task.Delay(10);
                return 10;
            }
        }
    }
}
