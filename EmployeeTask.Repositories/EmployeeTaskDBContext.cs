using EmployeeTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTask.Repositories
{
    public class EmployeeTaskDBContext : DbContext
    {
        public EmployeeTaskDBContext(DbContextOptions<EmployeeTaskDBContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeTemparature> Temparatures { get; set; }
    }
}
