using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTask.Shared.DataTrasferObject
{
    public class CommandDTO
    {
        [NotMapped]
        public string SuccessMessage { get; set; }
        [NotMapped]
        public string ErrorMessage { get; set; }
        [NotMapped]
        public bool DeleteCommand { get; set; } = false;
        public bool AddCommand { get; set; } = false;
        public bool UpdateCommand { get; set; } = false;
    }
}
