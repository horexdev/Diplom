using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetkServer.Migrations
{
    public partial class Add_ReferenceOfEmployeeToEnterExitHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EnterExitHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EnterExitHistory_EmployeeId",
                table: "EnterExitHistory",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnterExitHistory_Employees_EmployeeId",
                table: "EnterExitHistory",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnterExitHistory_Employees_EmployeeId",
                table: "EnterExitHistory");

            migrationBuilder.DropIndex(
                name: "IX_EnterExitHistory_EmployeeId",
                table: "EnterExitHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EnterExitHistory");

            migrationBuilder.AddColumn<int>(
                name: "EnterExitHistoryId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EnterExitHistoryId",
                table: "Employees",
                column: "EnterExitHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EnterExitHistory_EnterExitHistoryId",
                table: "Employees",
                column: "EnterExitHistoryId",
                principalTable: "EnterExitHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
