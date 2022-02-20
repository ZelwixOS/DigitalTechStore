namespace Infrastructure.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CommonCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechParameters_Categories_CategoryIdFk",
                table: "TechParameters");

            migrationBuilder.RenameColumn(
                name: "CategoryIdFk",
                table: "TechParameters",
                newName: "ParameterBlockIdFk");

            migrationBuilder.RenameIndex(
                name: "IX_TechParameters_CategoryIdFk",
                table: "TechParameters",
                newName: "IX_TechParameters_ParameterBlockIdFk");

            migrationBuilder.AddColumn<int>(
                name: "ParameterType",
                table: "TechParameters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CommonCategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CommonCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParameterBlock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterBlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterBlock_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryParameterBlock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Important = table.Column<bool>(type: "bit", nullable: false),
                    CategoryIdFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParameterBlockIdFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryParameterBlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryParameterBlock_Categories_CategoryIdFk",
                        column: x => x.CategoryIdFk,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryParameterBlock_ParameterBlock_ParameterBlockIdFk",
                        column: x => x.ParameterBlockIdFk,
                        principalTable: "ParameterBlock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CommonCategoryId",
                table: "Categories",
                column: "CommonCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryParameterBlock_CategoryIdFk",
                table: "CategoryParameterBlock",
                column: "CategoryIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryParameterBlock_ParameterBlockIdFk",
                table: "CategoryParameterBlock",
                column: "ParameterBlockIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterBlock_CategoryId",
                table: "ParameterBlock",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CommonCategory_CommonCategoryId",
                table: "Categories",
                column: "CommonCategoryId",
                principalTable: "CommonCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechParameters_ParameterBlock_ParameterBlockIdFk",
                table: "TechParameters",
                column: "ParameterBlockIdFk",
                principalTable: "ParameterBlock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CommonCategory_CommonCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_TechParameters_ParameterBlock_ParameterBlockIdFk",
                table: "TechParameters");

            migrationBuilder.DropTable(
                name: "CategoryParameterBlock");

            migrationBuilder.DropTable(
                name: "CommonCategory");

            migrationBuilder.DropTable(
                name: "ParameterBlock");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CommonCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParameterType",
                table: "TechParameters");

            migrationBuilder.DropColumn(
                name: "CommonCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParameterBlockIdFk",
                table: "TechParameters",
                newName: "CategoryIdFk");

            migrationBuilder.RenameIndex(
                name: "IX_TechParameters_ParameterBlockIdFk",
                table: "TechParameters",
                newName: "IX_TechParameters_CategoryIdFk");

            migrationBuilder.AddForeignKey(
                name: "FK_TechParameters_Categories_CategoryIdFk",
                table: "TechParameters",
                column: "CategoryIdFk",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
