using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AmfValor.AmfMoney.PortalApi.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Birth = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    HashedPassword = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    HashedPin = table.Column<byte[]>(nullable: false),
                    PinSalt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradingBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    AmountPerCaptal = table.Column<decimal>(type: "decimal(2,2)", nullable: false),
                    RiskRewardRatio = table.Column<sbyte>(type: "tinyint(1)", nullable: false),
                    TotalCaptal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RiskPerTrade = table.Column<decimal>(type: "decimal(2,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradingBooks_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OperationType = table.Column<string>(nullable: false),
                    Asset = table.Column<string>(maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    StopLoss = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    StopGain = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TradingBookEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trades_TradingBooks_TradingBookEntityId",
                        column: x => x.TradingBookEntityId,
                        principalTable: "TradingBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trades_TradingBookEntityId",
                table: "Trades",
                column: "TradingBookEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TradingBooks_AccountId",
                table: "TradingBooks",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "TradingBooks");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
