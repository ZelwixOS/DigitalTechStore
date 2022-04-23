namespace Infrastructure.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class NullifiedValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductParameters_ParameterValues_ParameterValueIdFk",
                table: "ProductParameters");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParameterValueIdFk",
                table: "ProductParameters",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductParameters_ParameterValues_ParameterValueIdFk",
                table: "ProductParameters",
                column: "ParameterValueIdFk",
                principalTable: "ParameterValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductParameters_ParameterValues_ParameterValueIdFk",
                table: "ProductParameters");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParameterValueIdFk",
                table: "ProductParameters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductParameters_ParameterValues_ParameterValueIdFk",
                table: "ProductParameters",
                column: "ParameterValueIdFk",
                principalTable: "ParameterValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
