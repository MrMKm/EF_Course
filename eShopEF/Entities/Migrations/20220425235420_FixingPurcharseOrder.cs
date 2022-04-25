using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class FixingPurcharseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminOrderProduct_CustomerOrder_CustomerOrderID",
                table: "AdminOrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_AdminOrderProduct_CustomerOrderID",
                table: "AdminOrderProduct");

            migrationBuilder.DropColumn(
                name: "AdminOrderProductsID",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "CustomerOrderID",
                table: "AdminOrderProduct");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderProduct_ProductID",
                table: "CustomerOrderProduct",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrderProduct_Product_ProductID",
                table: "CustomerOrderProduct",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrderProduct_Product_ProductID",
                table: "CustomerOrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrderProduct_ProductID",
                table: "CustomerOrderProduct");

            migrationBuilder.AddColumn<int>(
                name: "AdminOrderProductsID",
                table: "PurchaseOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerOrderID",
                table: "AdminOrderProduct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminOrderProduct_CustomerOrderID",
                table: "AdminOrderProduct",
                column: "CustomerOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminOrderProduct_CustomerOrder_CustomerOrderID",
                table: "AdminOrderProduct",
                column: "CustomerOrderID",
                principalTable: "CustomerOrder",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
