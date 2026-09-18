using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiveToolHomeCare.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCustomerServicePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicePrice",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ServicePrice",
                table: "Customers",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
