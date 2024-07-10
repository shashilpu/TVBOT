using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class addednewquery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TickerInfos",
                columns: table => new
                {
                    TickerInfoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CampanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NseSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MCSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerInfos", x => x.TickerInfoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TickerInfos");
        }
    }
}
