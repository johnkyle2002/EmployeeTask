// See https://aka.ms/new-console-template for more information
using EmployeeTask.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
 
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var configuration = builder.Build();

var optionBuilder = new DbContextOptionsBuilder<EmployeeTaskDBContext>();
optionBuilder.UseSqlServer(configuration.GetConnectionString("EmployeeTaskDBContext"), b => b.MigrationsAssembly("EmployeeTask.DBMigration"));

public class EmployeeTaskDBContextFactory : IDesignTimeDbContextFactory<EmployeeTaskDBContext>
{
    public EmployeeTaskDBContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();

        var optionsBuilder = new DbContextOptionsBuilder<EmployeeTaskDBContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("EmployeeTaskDBContext"), b => b.MigrationsAssembly("EmployeeTask.DBMigration"));

        return new EmployeeTaskDBContext(optionsBuilder.Options);
    }
}