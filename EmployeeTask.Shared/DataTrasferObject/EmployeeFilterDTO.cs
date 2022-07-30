using EmployeeTask.Shared.Enumerator;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Shared.DataTrasferObject
{
    public class EmployeeFilterDTO
    {
        [Required]
        public EmployeeFilterEnum.Employee FilterBy { get; set; }
        public string FilterValue { get; set; } = String.Empty;
        public decimal StartDecimal { get; set; } = 36m;
        public decimal EndDecimal { get; set; } = 36m;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
    }
}
