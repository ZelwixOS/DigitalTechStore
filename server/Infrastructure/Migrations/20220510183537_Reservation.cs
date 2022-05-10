namespace Infrastructure.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountPrice",
                table: "Products",
                newName: "PriceWithoutDiscount");

            migrationBuilder.CreateTable(
                name: "OutletsReserved",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutletId = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutletsReserved", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutletsReserved_Outlets_OutletId",
                        column: x => x.OutletId,
                        principalTable: "Outlets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutletsReserved_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutletsReserved_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehousesReserved",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehousesReserved", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehousesReserved_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehousesReserved_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehousesReserved_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutletsReserved_OutletId",
                table: "OutletsReserved",
                column: "OutletId");

            migrationBuilder.CreateIndex(
                name: "IX_OutletsReserved_ProductId",
                table: "OutletsReserved",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OutletsReserved_PurchaseId",
                table: "OutletsReserved",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousesReserved_ProductId",
                table: "WarehousesReserved",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousesReserved_PurchaseId",
                table: "WarehousesReserved",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousesReserved_WarehouseId",
                table: "WarehousesReserved",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutletsReserved");

            migrationBuilder.DropTable(
                name: "WarehousesReserved");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "Products",
                newName: "DiscountPrice");
        }
    }
}
