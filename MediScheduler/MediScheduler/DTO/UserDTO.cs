using System.ComponentModel.DataAnnotations;

namespace MediScheduler.DTO
{
    public class UserDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter User Name")]
        public string? UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Password")]
        public string? Password { get; set; }
        public int userId { get; set; }
    }
}
