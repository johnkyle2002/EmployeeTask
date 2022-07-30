using System.ComponentModel;

namespace EmployeeTask.Shared.Enumerator
{
    public class EmployeeFilterEnum
    {
        public enum Employee
        {
            [Description("First Name")]
            FirstName,
            [Description("Last Name")]
            LastName,
            Temperature,
            [Description("Record Date")]
            RecordDate
        }
    }
}
