using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTask.Shared.DataTrasferObject
{
    public class EmployeeDTO
    {
        public int EmployeeNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public decimal Temperature { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
