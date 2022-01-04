namespace Infrastructure.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Mark = table.Column<double>(type: "float", nullable: false),
                    VendorCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Popularity = table.Column<int>(type: "int", nullable: false),
                    PicURL = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CategoryIdFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryIdFk",
                        column: x => x.CategoryIdFk,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Important = table.Column<bool>(type: "bit", nullable: false),
                    CategoryIdFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechParameters_Categories_CategoryIdFk",
                        column: x => x.CategoryIdFk,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ParameterIdFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductIdFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductParameters_Products_ProductIdFk",
                        column: x => x.ProductIdFk,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductParameters_TechParameters_ParameterIdFk",
                        column: x => x.ParameterIdFk,
                        principalTable: "TechParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductParameters_ParameterIdFk",
                table: "ProductParameters",
                column: "ParameterIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParameters_ProductIdFk",
                table: "ProductParameters",
                column: "ProductIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryIdFk",
                table: "Products",
                column: "CategoryIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_TechParameters_CategoryIdFk",
                table: "TechParameters",
                column: "CategoryIdFk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductParameters");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TechParameters");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
