using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Coupons.PepsiKSA.Api.Migrations
{
    public partial class AddRegisterDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 13, 23, 11, 36, 92, DateTimeKind.Local).AddTicks(1241));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "Users");
        }
    }
}
