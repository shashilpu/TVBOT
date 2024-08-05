using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class initialmingrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Volume",
                table: "TradeOpportunity",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TradeOpportunity",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentChange",
                table: "TradeOpportunity",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Volume",
                table: "TradeOpportunity",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TradeOpportunity",
                type: "decimal(3,2)",
                precision: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentChange",
                table: "TradeOpportunity",
                type: "decimal(3,2)",
                precision: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);
        }
    }
}
