using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Coupons.PepsiKSA.Api.Migrations
{
    public partial class FixDateDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegisterDate",
                table: "Users",
                nullable: true,
                defaultValue: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 27, 22, 42, 43, 650, DateTimeKind.Local).AddTicks(1344));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 27, 22, 42, 43, 650, DateTimeKind.Local).AddTicks(1344),
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "getdate()");
        }
    }
}
