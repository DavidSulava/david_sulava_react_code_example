using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.Contractor.Core.Migrations
{
    public partial class product_currentversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentVersionId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CurrentVersionId",
                table: "Products",
                column: "CurrentVersionId",
                unique: true,
                filter: "[CurrentVersionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductVersions_CurrentVersionId",
                table: "Products",
                column: "CurrentVersionId",
                principalTable: "ProductVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductVersions_CurrentVersionId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CurrentVersionId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CurrentVersionId",
                table: "Products");
        }
    }
}
