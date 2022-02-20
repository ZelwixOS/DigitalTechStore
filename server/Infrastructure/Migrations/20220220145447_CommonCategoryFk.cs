namespace Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CommonCategoryFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CommonCategory_CommonCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CommonCategoryId",
                table: "Categories",
                newName: "CommonCategoryIdFk");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CommonCategoryId",
                table: "Categories",
                newName: "IX_Categories_CommonCategoryIdFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CommonCategory_CommonCategoryIdFk",
                table: "Categories",
                column: "CommonCategoryIdFk",
                principalTable: "CommonCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CommonCategory_CommonCategoryIdFk",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CommonCategoryIdFk",
                table: "Categories",
                newName: "CommonCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CommonCategoryIdFk",
                table: "Categories",
                newName: "IX_Categories_CommonCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CommonCategory_CommonCategoryId",
                table: "Categories",
                column: "CommonCategoryId",
                principalTable: "CommonCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
