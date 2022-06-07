using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class onfiguration_appbundle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_AppBundles_AppBundleId",
                table: "Configurations");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_AppBundleId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "AppBundleId",
                table: "Configurations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppBundleId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_AppBundleId",
                table: "Configurations",
                column: "AppBundleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_AppBundles_AppBundleId",
                table: "Configurations",
                column: "AppBundleId",
                principalTable: "AppBundles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
