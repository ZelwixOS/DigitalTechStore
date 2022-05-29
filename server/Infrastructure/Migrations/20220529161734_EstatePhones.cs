namespace Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EstatePhones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Outlets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Outlets");
        }
    }
}
