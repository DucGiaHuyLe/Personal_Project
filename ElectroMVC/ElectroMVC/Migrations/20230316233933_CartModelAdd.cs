using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroMVC.Migrations
{
    public partial class CartModelAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreChecked",
                table: "Category");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Cart_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.AddColumn<bool>(
                name: "AreChecked",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
