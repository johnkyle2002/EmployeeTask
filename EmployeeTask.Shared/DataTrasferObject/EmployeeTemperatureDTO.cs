using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Shared.DataTrasferObject
{
    public class EmployeeTemperatureDTO
    {
        public int EmployeeTemperatureID { get; set; }
        [Required]
        public int EmployeeNumber { get; set; }
        [Required]
        public decimal Temperature { get; set; }
        [Required]
        public DateTime RecordDate { get; set; }


        public decimal TemperatureF => 32 + (decimal)(Temperature / 0.5556m);
    }
}
