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

        [InverseProperty(nameof(EmployeeTemparature.Employee))]
        public virtual ICollection<EmployeeTemparature>? Temparatures { get; set; }
    }

}
