using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCBA_Model.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoginSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginState",
                table: "Logins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginState",
                table: "Logins");
        }
    }
}
