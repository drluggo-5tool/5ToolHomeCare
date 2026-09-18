using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiveToolHomeCare.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceIntervalMonths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceIntervalMonths",
                table: "ServiceContracts",
                type: "int",
                nullable: false,
                defaultValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceIntervalMonths",
                table: "ServiceContracts");
        }
    }
}
