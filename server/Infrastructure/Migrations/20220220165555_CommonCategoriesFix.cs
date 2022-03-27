namespace Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CommonCategoriesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CommonCategory_CommonCategoryIdFk",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommonCategory",
                table: "CommonCategory");

            migrationBuilder.RenameTable(
                name: "CommonCategory",
                newName: "CommonCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommonCategories",
                table: "CommonCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CommonCategories_CommonCategoryIdFk",
                table: "Categories",
                column: "CommonCategoryIdFk",
                principalTable: "CommonCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CommonCategories_CommonCategoryIdFk",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommonCategories",
                table: "CommonCategories");

            migrationBuilder.RenameTable(
                name: "CommonCategories",
                newName: "CommonCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommonCategory",
                table: "CommonCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CommonCategory_CommonCategoryIdFk",
                table: "Categories",
                column: "CommonCategoryIdFk",
                principalTable: "CommonCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
