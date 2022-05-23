using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class appbundle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentDefinitions_AppbBundles_AppBundleId",
                table: "ComponentDefinitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppbBundles",
                table: "AppbBundles");

            migrationBuilder.RenameTable(
                name: "AppbBundles",
                newName: "AppBundles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppBundles",
                table: "AppBundles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentDefinitions_AppBundles_AppBundleId",
                table: "ComponentDefinitions",
                column: "AppBundleId",
                principalTable: "AppBundles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentDefinitions_AppBundles_AppBundleId",
                table: "ComponentDefinitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppBundles",
                table: "AppBundles");

            migrationBuilder.RenameTable(
                name: "AppBundles",
                newName: "AppbBundles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppbBundles",
                table: "AppbBundles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentDefinitions_AppbBundles_AppBundleId",
                table: "ComponentDefinitions",
                column: "AppBundleId",
                principalTable: "AppbBundles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
