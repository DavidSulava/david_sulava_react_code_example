using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class configuration_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppBundleId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "WorkItemId",
                table: "Configurations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkItemUrl",
                table: "Configurations",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "WorkItemId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "WorkItemUrl",
                table: "Configurations");
        }
    }
}
