namespace Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixEstate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Outlets_Regions_CityId",
                table: "Outlets");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Regions_CityId",
                table: "Warehouses");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Warehouses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Outlets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_RegionId",
                table: "Warehouses",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Outlets_RegionId",
                table: "Outlets",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Outlets_Regions_RegionId",
                table: "Outlets",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Regions_RegionId",
                table: "Warehouses",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Outlets_Regions_RegionId",
                table: "Outlets");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Regions_RegionId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_RegionId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Outlets_RegionId",
                table: "Outlets");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Outlets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Outlets_Regions_CityId",
                table: "Outlets",
                column: "CityId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Regions_CityId",
                table: "Warehouses",
                column: "CityId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
