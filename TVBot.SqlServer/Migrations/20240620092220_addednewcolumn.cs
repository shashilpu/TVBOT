using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class addednewcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MCTicker",
                table: "TradeExecution",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TargetPercentGain",
                table: "TradeExecution",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TrargetPrice",
                table: "TradeExecution",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MCTicker",
                table: "TradeExecution");

            migrationBuilder.DropColumn(
                name: "TargetPercentGain",
                table: "TradeExecution");

            migrationBuilder.DropColumn(
                name: "TrargetPrice",
                table: "TradeExecution");
        }
    }
}
