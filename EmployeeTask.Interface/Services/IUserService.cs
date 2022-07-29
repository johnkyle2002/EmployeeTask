using EmployeeTask.Shared.DataTrasferObject;

namespace EmployeeTask.Interface.Services
{
    public interface IUserService
    {
        IOperationResult<UserDTO> GetLogin(string userName, string password);
    }
}
