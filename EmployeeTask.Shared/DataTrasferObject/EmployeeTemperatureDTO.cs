using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTask.Shared.DataTrasferObject
{
    public class EmployeeTemperatureDTO
    {
        public int EmployeeTemparatureID { get; set; }
        public int EmployeeNumber { get; set; }
        public decimal Temperature { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
