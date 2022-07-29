using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTask.Shared.DataTrasferObject
{
    public class EmployeeDTO
    {
        public int EmployeeNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }

        public virtual ICollection<EmployeeTemperatureDTO> Temperature { get; set; }
    }
}
