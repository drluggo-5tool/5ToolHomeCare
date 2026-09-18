using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiveToolHomeCare.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceContracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceContracts",
                columns: table => new
                {
                    ServiceContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnnualFilterCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AnnualFilterPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AnnualServiceFee = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceContracts", x => x.ServiceContractId);
                    table.ForeignKey(
                        name: "FK_ServiceContracts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceContracts_CustomerId",
                table: "ServiceContracts",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceContracts");
        }
    }
}
