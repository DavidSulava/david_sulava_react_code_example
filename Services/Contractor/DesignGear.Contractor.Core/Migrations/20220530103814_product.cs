using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.Contractor.Core.Migrations
{
    public partial class product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductVersions_CurrentVersionId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductVersions_CurrentVersionId",
                table: "Products",
                column: "CurrentVersionId",
                principalTable: "ProductVersions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductVersions_CurrentVersionId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductVersions_CurrentVersionId",
                table: "Products",
                column: "CurrentVersionId",
                principalTable: "ProductVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
