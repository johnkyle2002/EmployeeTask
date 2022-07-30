using EmployeeTask.Interface.Services;
using EmployeeTask.Shared.DataTrasferObject;
using EmployeeTask.Shared.Enumerator;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmployeeTask.BlazorClient.FactoryService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        public EmployeeService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7250/api/");
        }

        public async Task<IOperationResult<int?>> Create(EmployeeDTO employee)
        {
            var request = await _client.PostAsJsonAsync("Employee", employee);

            var content = await request.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<OperationResult<int?>>(content);

            if (result is null)
                result = new OperationResult<int?>
                {
                    StatusCode = StatusCodeEnum.Code.BadRequest,
                    Message = "Unable to retrieved record"
                };

            return result;
        }

        public async Task<IOperationResult<EmployeeDTO>> Get(int empNumber)
        {
            var request = await _client.GetFromJsonAsync<OperationResult<EmployeeDTO>>($"Employee/Get/{empNumber}", options);

            if (request is null)
                request = new OperationResult<EmployeeDTO>
                {
                    StatusCode = StatusCodeEnum.Code.BadRequest,
                    Message = "Unable to retrieved record"
                };

            return request;
        }

        public async Task<IOperationResult<IList<EmployeeDTO>>> GetAll()
        {
            var request = await _client.GetFromJsonAsync<OperationResult<IList<EmployeeDTO>>>("Employee/GetAll", options);

            if (request is null)
                request = new OperationResult<IList<EmployeeDTO>>
                {
                    StatusCode = StatusCodeEnum.Code.BadRequest,
                    Message = "Unable to retrieved record"
                };

            return request;
        }

        public async Task<IOperationResult<IList<EmployeeDTO>>> GetAllByFilter(IList<EmployeeFilterDTO> filters)
        {
            var request = await _client.PostAsJsonAsync("https://localhost:7250/api/Employee/GetByFilter", filters);

            var content = await request.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<OperationResult<IList<EmployeeDTO>>>(content);

            if (result is null)
                result = new OperationResult<IList<EmployeeDTO>>
                {
                    StatusCode = StatusCodeEnum.Code.BadRequest,
                    Message = "Unable to retrieved record"
                };

            return result;

        }

        public async Task<IOperationResult> Remove(int empNumber)
        {
            var request = await _client.DeleteAsync($"Employee/{empNumber}");
            var content = await request.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<OperationResult>(content);

            if (result is null)
                result = new OperationResult
                {
                    StatusCode = StatusCodeEnum.Code.BadRequest,
                    Message = "Unable to delete record"
                };

            return result;
        }

        public async Task<IOperationResult> Update(EmployeeDTO employee)
        {
            var request = await _client.PutAsJsonAsync("Employee", employee);

            var content = await request.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<OperationResult>(content);

            if (result is null)
                result = new OperationResult
                {
                    StatusCode = StatusCodeEnum.Code.BadRequest,
                    Message = "Unable to update record"
                };

            return result;
        }
    }
}
