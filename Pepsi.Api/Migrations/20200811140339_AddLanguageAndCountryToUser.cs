using Microsoft.EntityFrameworkCore.Migrations;

namespace Coupons.PepsiKSA.Api.Migrations
{
    public partial class AddLanguageAndCountryToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Users",
                maxLength: 20,
                nullable: true,
                defaultValue: "SA");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Users",
                maxLength: 10,
                nullable: true,
                defaultValue: "Arabic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Users");
        }
    }
}
