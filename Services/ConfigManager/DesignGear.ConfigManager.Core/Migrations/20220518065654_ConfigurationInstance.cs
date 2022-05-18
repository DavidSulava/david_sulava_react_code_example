using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class ConfigurationInstance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentConfigurationId",
                table: "ConfigurationInstances");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationInstances_ConfigurationId",
                table: "ConfigurationInstances",
                column: "ConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationInstances_Configurations_ConfigurationId",
                table: "ConfigurationInstances",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationInstances_Configurations_ConfigurationId",
                table: "ConfigurationInstances");

            migrationBuilder.DropIndex(
                name: "IX_ConfigurationInstances_ConfigurationId",
                table: "ConfigurationInstances");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentConfigurationId",
                table: "ConfigurationInstances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
