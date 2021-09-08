using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageAndStorage.Migrations
{
    public partial class New3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ExpiredDate",
                table: "Items",
                newName: "SaleDate");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfItems",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Sum",
                table: "Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfItems",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Sum",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "Items",
                newName: "ExpiredDate");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
