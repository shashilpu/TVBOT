using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class madestoplossmandatory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "StopLossPrice",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "StopLossPrice",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4);
        }
    }
}
