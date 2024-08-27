using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ssd_task.Migrations
{
    /// <inheritdoc />
    public partial class initialcomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Personnel_Records = table.Column<string>(type: "varchar(450)", nullable: false),
                    Forenames = table.Column<string>(type: "varchar(450)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(450)", nullable: false),
                    Date_of_Birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Telephone = table.Column<string>(type: "varchar(450)", nullable: false),
                    Mobile = table.Column<string>(type: "varchar(450)", nullable: false),
                    Address = table.Column<string>(type: "varchar(450)", nullable: false),
                    Address_2 = table.Column<string>(type: "varchar(450)", nullable: false),
                    Postcode = table.Column<string>(type: "varchar(450)", nullable: false),
                    EMail_Home = table.Column<string>(type: "varchar(450)", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Personnel_Records);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
