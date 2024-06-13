using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedTradeExecutionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
