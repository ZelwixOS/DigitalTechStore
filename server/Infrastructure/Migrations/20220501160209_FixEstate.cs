namespace Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixEstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityName",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "RegionName",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "Outlets");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Outlets");

            migrationBuilder.DropColumn(
                name: "RegionName",
                table: "Outlets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionName",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "Outlets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Outlets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionName",
                table: "Outlets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
