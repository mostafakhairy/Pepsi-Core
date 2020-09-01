using Microsoft.EntityFrameworkCore.Migrations;

namespace Coupons.PepsiKSA.Api.Migrations
{
    public partial class IsUserSubscribedToSmsEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubscribedMail",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscribedSms",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscribedMail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsSubscribedSms",
                table: "Users");
        }
    }
}
