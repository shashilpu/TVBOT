using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class addedsupportforPriceCehck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MCTicker",
                table: "TradeExecution",
                newName: "Ticker");

            migrationBuilder.AddColumn<decimal>(
                name: "BetaOneYear",
                table: "TradeOpportunityOneMin",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentVolalityOneWeek",
                table: "TradeOpportunityOneMin",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BetaOneYear",
                table: "TradeOpportunity",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentVolalityOneWeek",
                table: "TradeOpportunity",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentProfitLoss",
                table: "TradeExecutionOneMin",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TradeCloseDateTime",
                table: "TradeExecutionOneMin",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TradeClosePrice",
                table: "TradeExecutionOneMin",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentProfitLoss",
                table: "TradeExecution",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TradeCloseDateTime",
                table: "TradeExecution",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TradeClosePrice",
                table: "TradeExecution",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetaOneYear",
                table: "TradeOpportunityOneMin");

            migrationBuilder.DropColumn(
                name: "PercentVolalityOneWeek",
                table: "TradeOpportunityOneMin");

            migrationBuilder.DropColumn(
                name: "BetaOneYear",
                table: "TradeOpportunity");

            migrationBuilder.DropColumn(
                name: "PercentVolalityOneWeek",
                table: "TradeOpportunity");

            migrationBuilder.DropColumn(
                name: "PercentProfitLoss",
                table: "TradeExecutionOneMin");

            migrationBuilder.DropColumn(
                name: "TradeCloseDateTime",
                table: "TradeExecutionOneMin");

            migrationBuilder.DropColumn(
                name: "TradeClosePrice",
                table: "TradeExecutionOneMin");

            migrationBuilder.DropColumn(
                name: "PercentProfitLoss",
                table: "TradeExecution");

            migrationBuilder.DropColumn(
                name: "TradeCloseDateTime",
                table: "TradeExecution");

            migrationBuilder.DropColumn(
                name: "TradeClosePrice",
                table: "TradeExecution");

            migrationBuilder.RenameColumn(
                name: "Ticker",
                table: "TradeExecution",
                newName: "MCTicker");
        }
    }
}
