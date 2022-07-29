using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Shared.DataTrasferObject
{
    public class EmployeeDTO
    {
        public int EmployeeNumber { get; set; }
        [Required(ErrorMessage ="First name field is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name field is required.")]
        public string Lastname { get; set; }

        public virtual ICollection<EmployeeTemperatureDTO> Temperature { get; set; }
    }
}
