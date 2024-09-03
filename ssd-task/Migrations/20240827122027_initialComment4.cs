using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ssd_task.Migrations
{
    /// <inheritdoc />
    public partial class initialComment4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Payroll_Number",
                table: "Employees",
                newName: "Personnel_Records");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Personnel_Records",
                table: "Employees",
                newName: "Payroll_Number");
        }
    }
}
