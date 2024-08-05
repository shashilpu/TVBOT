using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class initialmingration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeOpportunity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(3,2)", precision: 3, nullable: false),
                    PercentChange = table.Column<decimal>(type: "decimal(3,2)", precision: 3, nullable: false),
                    AnalystRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlgoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    BetaOneYear = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CrossOverDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CrossOverType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentVolalityOneWeek = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeOpportunity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeExecution",
                columns: table => new
                {
                    TradeExecutionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrargetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvestedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentProfitLossOnTrade = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TradeClosePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProfitLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PercentProfitLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExecutionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TradeCloseDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InTrade = table.Column<bool>(type: "bit", nullable: false),
                    TradeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetPercentGain = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsRepeatedTrade = table.Column<bool>(type: "bit", nullable: false),
                    TradeOpportunityId = table.Column<int>(type: "int", nullable: false),
                    ExecutionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTradeFromPastBullCross = table.Column<bool>(type: "bit", nullable: false),
                    PastBullCrossInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeExecution", x => x.TradeExecutionId);
                    table.ForeignKey(
                        name: "FK_TradeExecution_TradeOpportunity_TradeOpportunityId",
                        column: x => x.TradeOpportunityId,
                        principalTable: "TradeOpportunity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeExecution_TradeOpportunityId",
                table: "TradeExecution",
                column: "TradeOpportunityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeExecution");

            migrationBuilder.DropTable(
                name: "TradeOpportunity");
        }
    }
}
