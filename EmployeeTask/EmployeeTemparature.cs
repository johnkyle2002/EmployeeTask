using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTask.Models
{
    public class EmployeeTemparature
    {
        public int EmployeeTemparatureID { get; set; }
        public int EmployeeNumber { get; set; }
        public decimal Temperature { get; set; }
        public DateTime RecordDate { get; set; }

        [ForeignKey(nameof(EmployeeNumber))]
        public Employee? Employee { get; set; }
    }
}
