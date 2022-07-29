using EmployeeTask.Shared.Enumerator;

namespace EmployeeTask.Shared.DataTrasferObject
{

    public class OperationResult<T> : IOperationResult<T>
    {
        public T Entity { get; set; }
        public StatusCodeEnum.Code StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class OperationResult : IOperationResult
    {        
        public StatusCodeEnum.Code StatusCode { get; set; }
        public string Message { get; set; }
    }

    public interface IOperationResult<T> : IOperationResult
    {
        public T Entity { get; set; }

    }

    public interface IOperationResult
    {
        StatusCodeEnum.Code StatusCode { get; set; }
        string Message { get; set; }
    }
}
