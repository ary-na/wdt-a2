using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s3910902_a2.Migrations
{
    /// <inheritdoc />
    public partial class Validation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPay_Accounts_AccountNumber",
                table: "BillPay");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPay_Payee_PayeeID",
                table: "BillPay");

            migrationBuilder.DropIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payee",
                table: "Payee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillPay",
                table: "BillPay");

            migrationBuilder.RenameTable(
                name: "Payee",
                newName: "Payees");

            migrationBuilder.RenameTable(
                name: "BillPay",
                newName: "BillPays");

            migrationBuilder.RenameIndex(
                name: "IX_BillPay_PayeeID",
                table: "BillPays",
                newName: "IX_BillPays_PayeeID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPay_AccountNumber",
                table: "BillPays",
                newName: "IX_BillPays_AccountNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payees",
                table: "Payees",
                column: "PayeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillPays",
                table: "BillPays",
                column: "BillPayID");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPays_Accounts_AccountNumber",
                table: "BillPays",
                column: "AccountNumber",
                principalTable: "Accounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPays_Payees_PayeeID",
                table: "BillPays",
                column: "PayeeID",
                principalTable: "Payees",
                principalColumn: "PayeeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPays_Accounts_AccountNumber",
                table: "BillPays");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPays_Payees_PayeeID",
                table: "BillPays");

            migrationBuilder.DropIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payees",
                table: "Payees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillPays",
                table: "BillPays");

            migrationBuilder.RenameTable(
                name: "Payees",
                newName: "Payee");

            migrationBuilder.RenameTable(
                name: "BillPays",
                newName: "BillPay");

            migrationBuilder.RenameIndex(
                name: "IX_BillPays_PayeeID",
                table: "BillPay",
                newName: "IX_BillPay_PayeeID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPays_AccountNumber",
                table: "BillPay",
                newName: "IX_BillPay_AccountNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payee",
                table: "Payee",
                column: "PayeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillPay",
                table: "BillPay",
                column: "BillPayID");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins",
                column: "CustomerID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPay_Accounts_AccountNumber",
                table: "BillPay",
                column: "AccountNumber",
                principalTable: "Accounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPay_Payee_PayeeID",
                table: "BillPay",
                column: "PayeeID",
                principalTable: "Payee",
                principalColumn: "PayeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
