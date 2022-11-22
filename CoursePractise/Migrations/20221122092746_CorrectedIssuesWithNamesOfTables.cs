using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePractise.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedIssuesWithNamesOfTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CaregoryId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "CaregoryId",
                table: "Product",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CaregoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Product_Category_CategoryId",
            //    table: "Product");

            //migrationBuilder.RenameColumn(
            //    name: "CategoryId",
            //    table: "Product",
            //    newName: "CaregoryId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Product_CategoryId",
            //    table: "Product",
            //    newName: "IX_Product_CaregoryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Product_Category_CaregoryId",
            //    table: "Product",
            //    column: "CaregoryId",
            //    principalTable: "Category",
            //    principalColumn: "CategoryId",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
