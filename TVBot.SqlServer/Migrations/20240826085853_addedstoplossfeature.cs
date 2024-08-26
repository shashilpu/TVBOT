using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class addedstoplossfeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StopLossPrice",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 24);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StopLossPrice",
                table: "TradeExecution");
        }
    }
}
