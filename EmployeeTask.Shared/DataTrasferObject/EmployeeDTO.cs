using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Shared.DataTrasferObject
{
    public class EmployeeDTO : BaseDTO
    {
        public int EmployeeNumber { get; set; }
        [Required(ErrorMessage = "First name field is required.")]
        [RegularExpression(pattern: @"[A-Za-z\s]*", ErrorMessage = "Invalid input value")]
        public string FirstName { get; set; }
        [RegularExpression(pattern: @"[A-Za-z\s]*", ErrorMessage = "Invalid input value")]
        [Required(ErrorMessage = "Last name field is required.")]
        public string Lastname { get; set; }

        public virtual ICollection<EmployeeTemperatureDTO> Temperature { get; set; }
    }
}
