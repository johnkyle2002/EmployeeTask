using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTask.Shared.DataTrasferObject
{
    public class BaseDTO
    {
        [NotMapped]
        public string SuccessMessage { get; set; }
        [NotMapped]
        public string ErrorMessage { get; set; }
        [NotMapped]
        public bool Delete { get; set; } = false;
    }
}
