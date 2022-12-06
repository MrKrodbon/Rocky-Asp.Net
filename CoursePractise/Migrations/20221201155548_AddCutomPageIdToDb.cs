using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePractise.Migrations
{
    public partial class AddCutomPageIdToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomPageId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CustomPageId",
                table: "Product",
                column: "CustomPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CustomPage_CustomPageId",
                table: "Product",
                column: "CustomPageId",
                principalTable: "CustomPage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_CustomPage_CustomPageId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CustomPageId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CustomPageId",
                table: "Product");
        }
    }
}
