using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class addednewcolumninexectable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "TradeExecutionOneMin",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentProfitLossOnTrade",
                table: "TradeExecutionOneMin",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InvestedAmount",
                table: "TradeExecutionOneMin",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRepeatedTrade",
                table: "TradeExecutionOneMin",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "TradeExecution",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentProfitLossOnTrade",
                table: "TradeExecution",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InvestedAmount",
                table: "TradeExecution",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRepeatedTrade",
                table: "TradeExecution",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "TradeExecutionOneMin");

            migrationBuilder.DropColumn(
                name: "CurrentProfitLossOnTrade",
                table: "TradeExecutionOneMin");

            migrationBuilder.DropColumn(
                name: "InvestedAmount",
                table: "TradeExecutionOneMin");

            migrationBuilder.DropColumn(
                name: "IsRepeatedTrade",
                table: "TradeExecutionOneMin");

            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "TradeExecution");

            migrationBuilder.DropColumn(
                name: "CurrentProfitLossOnTrade",
                table: "TradeExecution");

            migrationBuilder.DropColumn(
                name: "InvestedAmount",
                table: "TradeExecution");

            migrationBuilder.DropColumn(
                name: "IsRepeatedTrade",
                table: "TradeExecution");
        }
    }
}
