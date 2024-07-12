using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TickerInfos",
                columns: table => new
                {
                    TickerInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NseSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MCSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerInfos", x => x.TickerInfoId);
                });

            migrationBuilder.CreateTable(
                name: "TradeOpportunity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrossOverDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AlgoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CrossOverType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeOpportunity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeOpportunityOneMin",
                columns: table => new
                {
                    TradeOpportunityOneMinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrossOverDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AlgoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CrossOverType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeOpportunityOneMin", x => x.TradeOpportunityOneMinId);
                });

            migrationBuilder.CreateTable(
                name: "TradeExecution",
                columns: table => new
                {
                    TradeExecutionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeOpportunityId = table.Column<int>(type: "int", nullable: false),
                    ExecutionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InTrade = table.Column<bool>(type: "bit", nullable: false),
                    TradeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfitLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrargetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TargetPercentGain = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MCTicker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "TradeExecutionOneMin",
                columns: table => new
                {
                    TradeExecutionOneMinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeOpportunityOneMinId = table.Column<int>(type: "int", nullable: false),
                    ExecutionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InTrade = table.Column<bool>(type: "bit", nullable: false),
                    TradeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfitLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrargetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TargetPercentGain = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeExecutionOneMin", x => x.TradeExecutionOneMinId);
                    table.ForeignKey(
                        name: "FK_TradeExecutionOneMin_TradeOpportunityOneMin_TradeOpportunityOneMinId",
                        column: x => x.TradeOpportunityOneMinId,
                        principalTable: "TradeOpportunityOneMin",
                        principalColumn: "TradeOpportunityOneMinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeExecution_TradeOpportunityId",
                table: "TradeExecution",
                column: "TradeOpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeExecutionOneMin_TradeOpportunityOneMinId",
                table: "TradeExecutionOneMin",
                column: "TradeOpportunityOneMinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TickerInfos");

            migrationBuilder.DropTable(
                name: "TradeExecution");

            migrationBuilder.DropTable(
                name: "TradeExecutionOneMin");

            migrationBuilder.DropTable(
                name: "TradeOpportunity");

            migrationBuilder.DropTable(
                name: "TradeOpportunityOneMin");
        }
    }
}
