using Microsoft.EntityFrameworkCore.Migrations;

namespace Coupons.PepsiKSA.Api.Migrations
{
    public partial class UpdateCountryDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Users",
                maxLength: 20,
                nullable: true,
                defaultValue: "Saudi Arabia",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValue: "SA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "SA",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValue: "Saudi Arabia");
        }
    }
}
