using EmployeeTask.Interface.Repositories;
using EmployeeTask.Interface.Services;
using EmployeeTask.Repositories;
using EmployeeTask.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

var services = builder.Services;

services.AddDbContext<EmployeeTaskDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeTaskDBContext"));
}, ServiceLifetime.Scoped);

services.AddScoped<IEmployeeService, EmployeeService>();
services.AddScoped<IEmployeeTemperatureService, EmployeeTemperatureService>();

services.AddScoped<IEmployeeRepository, EmployeeRepository>();
services.AddScoped<IEmployeeTemperatureRepository, EmployeeTemperatureRepository>();
services.AddScoped<IQueryableRepository, QueryableRepository>();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
