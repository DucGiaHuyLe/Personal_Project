using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroMVC.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreChecked",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AreChecked",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
