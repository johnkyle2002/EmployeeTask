using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTask.Models
{
    public class EmployeeTemperature
    {
        public int EmployeeTemperatureID { get; set; }
        public int EmployeeNumber { get; set; }
        public decimal Temperature { get; set; }
        public DateTime RecordDate { get; set; }

        [ForeignKey(nameof(EmployeeNumber))]
        public Employee? Employee { get; set; }
    }
}
