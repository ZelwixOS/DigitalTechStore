namespace Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CategoryBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryParameterBlock_Categories_CategoryIdFk",
                table: "CategoryParameterBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryParameterBlock_ParameterBlock_ParameterBlockIdFk",
                table: "CategoryParameterBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterBlock_Categories_CategoryId",
                table: "ParameterBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_TechParameters_ParameterBlock_ParameterBlockIdFk",
                table: "TechParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParameterBlock",
                table: "ParameterBlock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryParameterBlock",
                table: "CategoryParameterBlock");

            migrationBuilder.RenameTable(
                name: "ParameterBlock",
                newName: "ParameterBlocks");

            migrationBuilder.RenameTable(
                name: "CategoryParameterBlock",
                newName: "CategoryParameterBlocks");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterBlock_CategoryId",
                table: "ParameterBlocks",
                newName: "IX_ParameterBlocks_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryParameterBlock_ParameterBlockIdFk",
                table: "CategoryParameterBlocks",
                newName: "IX_CategoryParameterBlocks_ParameterBlockIdFk");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryParameterBlock_CategoryIdFk",
                table: "CategoryParameterBlocks",
                newName: "IX_CategoryParameterBlocks_CategoryIdFk");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParameterBlocks",
                table: "ParameterBlocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryParameterBlocks",
                table: "CategoryParameterBlocks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryParameterBlocks_Categories_CategoryIdFk",
                table: "CategoryParameterBlocks",
                column: "CategoryIdFk",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryParameterBlocks_ParameterBlocks_ParameterBlockIdFk",
                table: "CategoryParameterBlocks",
                column: "ParameterBlockIdFk",
                principalTable: "ParameterBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterBlocks_Categories_CategoryId",
                table: "ParameterBlocks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TechParameters_ParameterBlocks_ParameterBlockIdFk",
                table: "TechParameters",
                column: "ParameterBlockIdFk",
                principalTable: "ParameterBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryParameterBlocks_Categories_CategoryIdFk",
                table: "CategoryParameterBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryParameterBlocks_ParameterBlocks_ParameterBlockIdFk",
                table: "CategoryParameterBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterBlocks_Categories_CategoryId",
                table: "ParameterBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_TechParameters_ParameterBlocks_ParameterBlockIdFk",
                table: "TechParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParameterBlocks",
                table: "ParameterBlocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryParameterBlocks",
                table: "CategoryParameterBlocks");

            migrationBuilder.RenameTable(
                name: "ParameterBlocks",
                newName: "ParameterBlock");

            migrationBuilder.RenameTable(
                name: "CategoryParameterBlocks",
                newName: "CategoryParameterBlock");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterBlocks_CategoryId",
                table: "ParameterBlock",
                newName: "IX_ParameterBlock_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryParameterBlocks_ParameterBlockIdFk",
                table: "CategoryParameterBlock",
                newName: "IX_CategoryParameterBlock_ParameterBlockIdFk");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryParameterBlocks_CategoryIdFk",
                table: "CategoryParameterBlock",
                newName: "IX_CategoryParameterBlock_CategoryIdFk");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParameterBlock",
                table: "ParameterBlock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryParameterBlock",
                table: "CategoryParameterBlock",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryParameterBlock_Categories_CategoryIdFk",
                table: "CategoryParameterBlock",
                column: "CategoryIdFk",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryParameterBlock_ParameterBlock_ParameterBlockIdFk",
                table: "CategoryParameterBlock",
                column: "ParameterBlockIdFk",
                principalTable: "ParameterBlock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterBlock_Categories_CategoryId",
                table: "ParameterBlock",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TechParameters_ParameterBlock_ParameterBlockIdFk",
                table: "TechParameters",
                column: "ParameterBlockIdFk",
                principalTable: "ParameterBlock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
