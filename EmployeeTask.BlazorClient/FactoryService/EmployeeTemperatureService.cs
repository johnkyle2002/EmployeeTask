using EmployeeTask.Interface.Services;
using EmployeeTask.Shared.DataTrasferObject;
using EmployeeTask.Shared.Enumerator;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmployeeTask.BlazorClient.FactoryService
{
    public class EmployeeTemperatureService : IEmployeeTemperatureService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public EmployeeTemperatureService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7250/api/");
        }

        public async Task<IOperationResult<int?>> Create(EmployeeTemperatureDTO temperature)
        {
            var request = await _client.PostAsJsonAsync("Temperature", temperature);

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


        public async Task<IOperationResult<EmployeeTemperatureDTO>> Get(int tempID)
        {
            var request = await _client.GetFromJsonAsync<OperationResult<EmployeeTemperatureDTO>>($"Temperature/Get/{tempID}", options);

            if (request is null)
                request = new OperationResult<EmployeeTemperatureDTO>
                {
                    StatusCode = StatusCodeEnum.Code.BadRequest,
                    Message = "Unable to retrieved record"
                };

            return request;
        }

        public async Task<IOperationResult<IList<EmployeeTemperatureDTO>>> GetAll()
        {
            var request = await _client.GetFromJsonAsync<OperationResult<IList<EmployeeTemperatureDTO>>>("Temperature/GetAll", options);

            if (request is null)
                request = new OperationResult<IList<EmployeeTemperatureDTO>>
                {
                    StatusCode = StatusCodeEnum.Code.BadRequest,
                    Message = "Unable to retrieved record"
                };

            return request;
        }

        public async Task<IOperationResult> Remove(int tempID)
        {
            var request = await _client.DeleteAsync($"Temperature/{tempID}");
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

        public async Task<IOperationResult> Update(EmployeeTemperatureDTO temperature)
        {
            var request = await _client.PutAsJsonAsync("Temperature", temperature);

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
