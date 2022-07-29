using EmployeeTask.Interface.Services;
using EmployeeTask.Shared.Configuration;
using EmployeeTask.Shared.DataTrasferObject;
using EmployeeTask.Shared.Enumerator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeTask.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptions<JwtOptions> _options;

        public AuthController(IUserService userService,
            IOptions<JwtOptions> options)
        {
            _userService = userService;
            _options = options;
        }

        [HttpPost]
        public ActionResult<IOperationResult<string>> Login(UserLoginDTO user)
        {
            var result = _userService.GetLogin(user.Username, user.Password);

            if (result.StatusCode != StatusCodeEnum.Code.Ok)
                return Ok(new OperationResult<string>
                {
                    StatusCode = StatusCodeEnum.Code.BadRequest,
                    Message = "Not found."
                });

            var claims = new List<Claim> {
                new Claim(ClaimTypes.GivenName, result.Entity.FirstName),
                new Claim(ClaimTypes.Surname, result.Entity.LastName),
                new Claim(ClaimTypes.NameIdentifier, result.Entity.UserName),
                new Claim(ClaimTypes.Role, result.Entity.Role),

            };


            var jtoken = new JwtSecurityToken(
                    issuer: _options.Value.Issuer,
                    audience: _options.Value.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key)),
                        SecurityAlgorithms.HmacSha256)
                );

            var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(jtoken);

            return Ok(new OperationResult<string>
            {
                Entity = jwtTokenString,
                StatusCode = StatusCodeEnum.Code.Ok,
                Message = "Not found."
            });
        }
    }
}
