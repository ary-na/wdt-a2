using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCBA_Customer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBillPaySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillState",
                table: "BillPays",
                newName: "BillStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillStatus",
                table: "BillPays",
                newName: "BillState");
        }
    }
}
