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
            _client.BaseAddress = new Uri("https://localhost:7250");
        }

        public async Task<IOperationResult<int?>> Create(EmployeeDTO employee)
        {
            var request = await _client.PostAsJsonAsync("/api/Employee/create", employee);

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

        public Task<IOperationResult<EmployeeDTO>> Get(int empNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<IOperationResult<IList<EmployeeDTO>>> GetAll()
        {
            var request = await _client.GetFromJsonAsync<OperationResult<IList<EmployeeDTO>>>("https://localhost:7250/api/Employee/GetAll", options);

            if(request is null)
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

        public Task<IOperationResult> Remove(int empNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IOperationResult> Update(EmployeeDTO employee)
        {
            throw new NotImplementedException();
        }
    }
}
