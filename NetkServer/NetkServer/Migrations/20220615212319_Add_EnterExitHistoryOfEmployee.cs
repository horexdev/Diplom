using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetkServer.Migrations
{
    public partial class Add_EnterExitHistoryOfEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnterExitHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WhenEnter = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WhenExit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoesLeave = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterExitHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnterExitHistory");
        }
    }
}
