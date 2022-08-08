using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s3910902_a2.Migrations
{
    /// <inheritdoc />
    public partial class removedBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "Period",
                table: "BillPays",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "BillPays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "BillPays");

            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "BillPays",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
