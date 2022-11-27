using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AggregationApp.Migrations
{
    /// <inheritdoc />
    public partial class tablePresicionScaleChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Pplus",
                table: "AggregatedElectricities",
                type: "decimal(14,6)",
                precision: 14,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Pminus",
                table: "AggregatedElectricities",
                type: "decimal(14,6)",
                precision: 14,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Pplus",
                table: "AggregatedElectricities",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,6)",
                oldPrecision: 14,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Pminus",
                table: "AggregatedElectricities",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,6)",
                oldPrecision: 14,
                oldScale: 6);
        }
    }
}
