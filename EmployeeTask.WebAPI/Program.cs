using EmployeeTask.Interface.Repositories;
using EmployeeTask.Interface.Services;
using EmployeeTask.Repositories;
using EmployeeTask.Services;
using EmployeeTask.Shared.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
services.AddScoped<IUserService, UserService>();


services.AddOptions();
services.Configure<JwtOptions>(options => configuration.GetSection("Jwt").Bind(options));

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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type= ReferenceType.SecurityScheme
                }
            },
            new List<string>()
       }
    });
});
services.AddCors(policy =>
{
    policy.AddPolicy("_blazorWasm", builder =>
     builder.WithOrigins("https://localhost:7277")
      .SetIsOriginAllowed((host) => true) // this for using localhost address
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials());
});
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("_blazorWasm");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
