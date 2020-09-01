using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Coupons.PepsiKSA.Api.Migrations
{
    public partial class UserLastModificationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationDate",
                table: "Users",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModificationDate",
                table: "Users");
        }
    }
}
