using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetkServer.Migrations
{
    public partial class Add_EmployeeHistoryOfEnterExit_EmployeeGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnterExitHistory",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnterExitHistory",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Employees");
        }
    }
}
