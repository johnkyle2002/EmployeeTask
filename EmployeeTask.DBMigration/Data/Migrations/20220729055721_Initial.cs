using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeTask.DBMigration.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeNumber);
                });

            migrationBuilder.CreateTable(
                name: "Temparatures",
                columns: table => new
                {
                    EmployeeTemperatureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temparatures", x => x.EmployeeTemperatureID);
                    table.ForeignKey(
                        name: "FK_Temparatures_Employees_EmployeeNumber",
                        column: x => x.EmployeeNumber,
                        principalTable: "Employees",
                        principalColumn: "EmployeeNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Temparatures_EmployeeNumber",
                table: "Temparatures",
                column: "EmployeeNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temparatures");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
