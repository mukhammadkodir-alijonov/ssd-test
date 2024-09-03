using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ssd_task.Migrations
{
    /// <inheritdoc />
    public partial class InitialComment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Employees",
                newName: "Start_Date");

            migrationBuilder.RenameColumn(
                name: "EmailHome",
                table: "Employees",
                newName: "EMail_Home");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Employees",
                newName: "Date_of_Birth");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "Employees",
                newName: "Address_2");

            migrationBuilder.RenameColumn(
                name: "PayrollNumber",
                table: "Employees",
                newName: "Payroll_Number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start_Date",
                table: "Employees",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EMail_Home",
                table: "Employees",
                newName: "EmailHome");

            migrationBuilder.RenameColumn(
                name: "Date_of_Birth",
                table: "Employees",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Address_2",
                table: "Employees",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "Payroll_Number",
                table: "Employees",
                newName: "PayrollNumber");
        }
    }
}
