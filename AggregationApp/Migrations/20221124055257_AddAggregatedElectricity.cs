using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AggregationApp.Migrations
{
    /// <inheritdoc />
    public partial class AddAggregatedElectricity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Electricities");

            migrationBuilder.CreateTable(
                name: "AggregatedElectricities",
                columns: table => new
                {
                    TINKLAS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pplus = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Pminus = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AggregatedElectricities");

            migrationBuilder.CreateTable(
                name: "Electricities",
                columns: table => new
                {
                    OBJGVTIPAS = table.Column<string>(name: "OBJ_GV_TIPAS", type: "nvarchar(max)", nullable: true),
                    OBJNUMERIS = table.Column<string>(name: "OBJ_NUMERIS", type: "nvarchar(max)", nullable: true),
                    OBTPAVADINIMAS = table.Column<string>(name: "OBT_PAVADINIMAS", type: "nvarchar(max)", nullable: true),
                    PLT = table.Column<string>(name: "PL_T", type: "nvarchar(max)", nullable: true),
                    Pminus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pplus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TINKLAS = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
