using Microsoft.EntityFrameworkCore.Migrations;

namespace Coupons.PepsiKSA.Api.Migrations
{
    public partial class UserVerifiedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEcouponsVerified",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEcouponsVerified",
                table: "Users");
        }
    }
}
