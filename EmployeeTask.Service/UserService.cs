using EmployeeTask.Interface.Services;
using EmployeeTask.Shared.DataTrasferObject;
using EmployeeTask.Shared.Enumerator;

namespace EmployeeTask.Services
{
    public class UserService : IUserService
    {
        private List<UserDTO> Users = new List<UserDTO>()
        {
            new UserDTO { Id=1, UserName="jsmith", FirstName = "John", LastName="Smith", Password="User_PassW0rd", Role = "Administrator" },
            new UserDTO { Id=1, UserName="spiderman", FirstName = "Peter", LastName="Parker", Password="User_PassW0rd", Role = "User" }
        };

        public IOperationResult<UserDTO> GetLogin(string userName, string password)
        {
            var user = Users.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && x.Password == password);

            if (user is null)
                return new OperationResult<UserDTO>
                {
                    Message = "Not found",
                    StatusCode = StatusCodeEnum.Code.BadRequest
                };

            return new OperationResult<UserDTO>
            {
                Entity = user,
                StatusCode = StatusCodeEnum.Code.Ok
            };
        }
    }
}
