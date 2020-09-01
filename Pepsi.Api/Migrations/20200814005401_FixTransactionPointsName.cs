using Microsoft.EntityFrameworkCore.Migrations;

namespace Coupons.PepsiKSA.Api.Migrations
{
    public partial class FixTransactionPointsName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "points",
                table: "UserTransactions",
                newName: "Points");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "UserTransactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "UserTransactions");

            migrationBuilder.RenameColumn(
                name: "Points",
                table: "UserTransactions",
                newName: "points");
        }
    }
}
