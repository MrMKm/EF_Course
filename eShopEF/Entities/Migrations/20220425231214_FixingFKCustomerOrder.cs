using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class FixingFKCustomerOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDto");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminOrderProduct_CustomerOrder_CustomerOrderID",
                table: "AdminOrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_AdminOrderProduct_CustomerOrderID",
                table: "AdminOrderProduct");

            migrationBuilder.DropColumn(
                name: "CustomerOrderID",
                table: "AdminOrderProduct");

            migrationBuilder.CreateTable(
                name: "ProductDto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerOrderID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    subDepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductDto_CustomerOrder_CustomerOrderID",
                        column: x => x.CustomerOrderID,
                        principalTable: "CustomerOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDto_CustomerOrderID",
                table: "ProductDto",
                column: "CustomerOrderID");
        }
    }
}
