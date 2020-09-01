using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Coupons.PepsiKSA.Api.Migrations
{
    public partial class UserTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Sku = table.Column<string>(maxLength: 50, nullable: true, defaultValue: "330ml"),
                    PromoCode = table.Column<string>(maxLength: 50, nullable: true),
                    OldPointsBalance = table.Column<int>(nullable: false),
                    NewPointsBalance = table.Column<int>(nullable: false),
                    points = table.Column<int>(nullable: false),
                    UserEmail = table.Column<string>(maxLength: 100, nullable: false),
                    ProductId = table.Column<string>(maxLength: 30, nullable: true, defaultValue: "Pepsi"),
                    CategoryId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CampaignId = table.Column<string>(maxLength: 50, nullable: true, defaultValue: "KSAPepsiPromo2020"),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTransactions_UserId",
                table: "UserTransactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTransactions");
        }
    }
}
