using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTask.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        [InverseProperty(nameof(EmployeeTemperature.Employee))]
        public virtual ICollection<EmployeeTemperature>? Temparatures { get; set; }
    }

}
