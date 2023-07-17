using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiProject.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensorDetailsNew",
                columns: table => new
                {
                    SensorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SensorType = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Pressure = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Temperature = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SupplyVoltageLevel = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Accuracy = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorDetailsNew", x => x.SensorId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorDetailsNew");
        }
    }
}