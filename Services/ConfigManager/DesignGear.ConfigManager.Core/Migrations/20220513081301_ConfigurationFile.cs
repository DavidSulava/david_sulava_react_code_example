using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class ConfigurationFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Configurations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SvfStatus",
                table: "Configurations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentComponentDefinitionId",
                table: "ComponentDefinitions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConfigurationFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationFiles_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentDefinitions_ParentComponentDefinitionId",
                table: "ComponentDefinitions",
                column: "ParentComponentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationFiles_ConfigurationId",
                table: "ConfigurationFiles",
                column: "ConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentDefinitions_ComponentDefinitions_ParentComponentDefinitionId",
                table: "ComponentDefinitions",
                column: "ParentComponentDefinitionId",
                principalTable: "ComponentDefinitions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentDefinitions_ComponentDefinitions_ParentComponentDefinitionId",
                table: "ComponentDefinitions");

            migrationBuilder.DropTable(
                name: "ConfigurationFiles");

            migrationBuilder.DropIndex(
                name: "IX_ComponentDefinitions_ParentComponentDefinitionId",
                table: "ComponentDefinitions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "SvfStatus",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "ParentComponentDefinitionId",
                table: "ComponentDefinitions");
        }
    }
}
