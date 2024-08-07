﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class changedPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Volume",
                table: "TradeOpportunity",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TradeOpportunity",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentVolalityOneWeek",
                table: "TradeOpportunity",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentChange",
                table: "TradeOpportunity",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "BetaOneYear",
                table: "TradeOpportunity",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TrargetPrice",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TradeClosePrice",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetPercentGain",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProfitLoss",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentProfitLoss",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InvestedAmount",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExecutionPrice",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExecutionFee",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentProfitLossOnTrade",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "TradeExecution",
                type: "decimal(15,4)",
                precision: 15,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Volume",
                table: "TradeOpportunity",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TradeOpportunity",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentVolalityOneWeek",
                table: "TradeOpportunity",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentChange",
                table: "TradeOpportunity",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "BetaOneYear",
                table: "TradeOpportunity",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "TrargetPrice",
                table: "TradeExecution",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "TradeClosePrice",
                table: "TradeExecution",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetPercentGain",
                table: "TradeExecution",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "TradeExecution",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProfitLoss",
                table: "TradeExecution",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentProfitLoss",
                table: "TradeExecution",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InvestedAmount",
                table: "TradeExecution",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExecutionPrice",
                table: "TradeExecution",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExecutionFee",
                table: "TradeExecution",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentProfitLossOnTrade",
                table: "TradeExecution",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "TradeExecution",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,4)",
                oldPrecision: 15,
                oldScale: 4,
                oldNullable: true);
        }
    }
}
