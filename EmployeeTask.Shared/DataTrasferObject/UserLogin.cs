using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Shared.DataTrasferObject
{
    public class UserLoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
