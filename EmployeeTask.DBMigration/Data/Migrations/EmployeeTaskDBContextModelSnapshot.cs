// <auto-generated />
using System;
using EmployeeTask.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeTask.DBMigration.Data.Migrations
{
    [DbContext(typeof(EmployeeTaskDBContext))]
    partial class EmployeeTaskDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EmployeeTask.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeNumber"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeNumber");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeeTask.Models.EmployeeTemperature", b =>
                {
                    b.Property<int>("EmployeeTemperatureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeTemperatureID"), 1L, 1);

                    b.Property<int>("EmployeeNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("RecordDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Temperature")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("EmployeeTemperatureID");

                    b.HasIndex("EmployeeNumber");

                    b.ToTable("Temparatures");
                });

            modelBuilder.Entity("EmployeeTask.Models.EmployeeTemperature", b =>
                {
                    b.HasOne("EmployeeTask.Models.Employee", "Employee")
                        .WithMany("Temparatures")
                        .HasForeignKey("EmployeeNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("EmployeeTask.Models.Employee", b =>
                {
                    b.Navigation("Temparatures");
                });
#pragma warning restore 612, 618
        }
    }
}
