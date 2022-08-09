using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCBA_Customer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateValidations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostCode",
                table: "Customers",
                newName: "Postcode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Postcode",
                table: "Customers",
                newName: "PostCode");
        }
    }
}
