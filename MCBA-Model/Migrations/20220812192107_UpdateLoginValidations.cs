using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCBA_Model.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoginValidations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Logins",
                type: "char(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(64)",
                oldMaxLength: 64);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Logins",
                type: "char(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(64)",
                oldMaxLength: 64,
                oldNullable: true);
        }
    }
}
