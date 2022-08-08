using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s3910902_a2.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "BillPays",
                newName: "BillState");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillState",
                table: "BillPays",
                newName: "State");
        }
    }
}
