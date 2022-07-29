using Blazored.LocalStorage;
using EmployeeTask.Shared.DataTrasferObject;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace EmployeeTask.BlazorClient.Pages
{
    public partial class Login
    {
        [Inject] HttpClient _client { get; set; }
        [Inject] AuthenticationStateProvider _authProvider { get; set; }
        [Inject] ILocalStorageService _localStorageService { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }


        private UserLoginDTO user = new()
        {
            Username = "jsmith",
            Password = "User_PassW0rd"
        };


        async Task HandleLogin()
        {
            var result = await _client.PostAsJsonAsync("https://localhost:7250/api/auth", user);
            var token = await result.Content.ReadAsStringAsync();


            var requestResult = JsonConvert.DeserializeObject<OperationResult<string>>(token);

            if (requestResult?.StatusCode == EmployeeTask.Shared.Enumerator.StatusCodeEnum.Code.Ok)
            {
                await _localStorageService.SetItemAsStringAsync("token", requestResult.Entity);
                await _authProvider.GetAuthenticationStateAsync();
                _navigationManager.NavigateTo("");
            }
        }

    }
}