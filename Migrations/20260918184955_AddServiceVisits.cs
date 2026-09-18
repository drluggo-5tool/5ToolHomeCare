using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiveToolHomeCare.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceVisits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceVisits",
                columns: table => new
                {
                    ServiceVisitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ServiceContractId = table.Column<int>(type: "int", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceVisits", x => x.ServiceVisitId);
                    table.ForeignKey(
                        name: "FK_ServiceVisits_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_ServiceVisits_ServiceContracts_ServiceContractId",
                        column: x => x.ServiceContractId,
                        principalTable: "ServiceContracts",
                        principalColumn: "ServiceContractId");
                });

            migrationBuilder.CreateTable(
                name: "ServiceVisitPhotos",
                columns: table => new
                {
                    ServiceVisitPhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceVisitId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceVisitPhotos", x => x.ServiceVisitPhotoId);
                    table.ForeignKey(
                        name: "FK_ServiceVisitPhotos_ServiceVisits_ServiceVisitId",
                        column: x => x.ServiceVisitId,
                        principalTable: "ServiceVisits",
                        principalColumn: "ServiceVisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceVisitPhotos_ServiceVisitId",
                table: "ServiceVisitPhotos",
                column: "ServiceVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceVisits_CustomerId",
                table: "ServiceVisits",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceVisits_ServiceContractId",
                table: "ServiceVisits",
                column: "ServiceContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceVisitPhotos");

            migrationBuilder.DropTable(
                name: "ServiceVisits");
        }
    }
}
