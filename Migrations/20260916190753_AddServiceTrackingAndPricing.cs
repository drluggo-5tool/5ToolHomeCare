using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiveToolHomeCare.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTrackingAndPricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ServicePrice",
                table: "Customers",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FilterCost",
                table: "AirFilterDetails",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FilterPrice",
                table: "AirFilterDetails",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ServiceLogs",
                columns: table => new
                {
                    ServiceLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceItemId = table.Column<int>(type: "int", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilterCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FilterPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ServicePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLogs", x => x.ServiceLogId);
                    table.ForeignKey(
                        name: "FK_ServiceLogs_ServiceItems_ServiceItemId",
                        column: x => x.ServiceItemId,
                        principalTable: "ServiceItems",
                        principalColumn: "ServiceItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceLogs_ServiceItemId",
                table: "ServiceLogs",
                column: "ServiceItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceLogs");

            migrationBuilder.DropColumn(
                name: "ServicePrice",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FilterCost",
                table: "AirFilterDetails");

            migrationBuilder.DropColumn(
                name: "FilterPrice",
                table: "AirFilterDetails");
        }
    }
}
