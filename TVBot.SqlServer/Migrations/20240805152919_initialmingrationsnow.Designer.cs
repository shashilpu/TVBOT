﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TVBot.SqlServer;

#nullable disable

namespace TVBot.SqlServer.Migrations
{
    [DbContext(typeof(SqlServerDbContext))]
    [Migration("20240805152919_initialmingrationsnow")]
    partial class initialmingrationsnow
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TVBot.Model.Entities.TradeExecution", b =>
                {
                    b.Property<int>("TradeExecutionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TradeExecutionId"));

                    b.Property<decimal?>("CurrentPrice")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(6);

                    b.Property<decimal?>("CurrentProfitLossOnTrade")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(8);

                    b.Property<DateTime>("ExecutionDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(12);

                    b.Property<decimal?>("ExecutionFee")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(19);

                    b.Property<decimal>("ExecutionPrice")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(3);

                    b.Property<bool>("InTrade")
                        .HasColumnType("bit")
                        .HasColumnOrder(14);

                    b.Property<decimal?>("InvestedAmount")
                        .HasPrecision(15, 5)
                        .HasColumnType("decimal(15,5)")
                        .HasColumnOrder(7);

                    b.Property<bool>("IsRepeatedTrade")
                        .HasColumnType("bit")
                        .HasColumnOrder(17);

                    b.Property<bool>("IsTradeFromPastBullCross")
                        .HasColumnType("bit")
                        .HasColumnOrder(21);

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(20);

                    b.Property<string>("PastBullCrossInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(22);

                    b.Property<decimal?>("PercentProfitLoss")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(11);

                    b.Property<decimal?>("ProfitLoss")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(10);

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnOrder(4);

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(23);

                    b.Property<decimal>("TargetPercentGain")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(16);

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<DateTime?>("TradeCloseDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(13);

                    b.Property<decimal?>("TradeClosePrice")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(9);

                    b.Property<int>("TradeOpportunityId")
                        .HasColumnType("int")
                        .HasColumnOrder(18);

                    b.Property<string>("TradeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(15);

                    b.Property<decimal>("TrargetPrice")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(5);

                    b.HasKey("TradeExecutionId");

                    b.HasIndex("TradeOpportunityId");

                    b.ToTable("TradeExecution");
                });

            modelBuilder.Entity("TVBot.Model.Entities.TradeOpportunity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AlgoName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(6);

                    b.Property<string>("AnalystRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(5);

                    b.Property<decimal?>("BetaOneYear")
                        .IsRequired()
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(8);

                    b.Property<DateTime>("CrossOverDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnOrder(9);

                    b.Property<string>("CrossOverType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(10);

                    b.Property<decimal>("PercentChange")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(4);

                    b.Property<decimal?>("PercentVolalityOneWeek")
                        .IsRequired()
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(11);

                    b.Property<decimal?>("Price")
                        .IsRequired()
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(3);

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<decimal>("Volume")
                        .HasPrecision(9, 5)
                        .HasColumnType("decimal(9,5)")
                        .HasColumnOrder(7);

                    b.HasKey("Id");

                    b.ToTable("TradeOpportunity");
                });

            modelBuilder.Entity("TVBot.Model.Entities.TradeExecution", b =>
                {
                    b.HasOne("TVBot.Model.Entities.TradeOpportunity", "TradeOpportunity")
                        .WithMany()
                        .HasForeignKey("TradeOpportunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TradeOpportunity");
                });
#pragma warning restore 612, 618
        }
    }
}
