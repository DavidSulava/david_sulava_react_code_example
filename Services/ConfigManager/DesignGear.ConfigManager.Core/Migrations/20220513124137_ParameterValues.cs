using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class ParameterValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterValue_Configurations_ConfigurationId",
                table: "ParameterValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterValue_ParameterDefinitions_ParameterDefinitionId",
                table: "ParameterValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParameterValue",
                table: "ParameterValue");

            migrationBuilder.RenameTable(
                name: "ParameterValue",
                newName: "ParameterValues");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterValue_ParameterDefinitionId",
                table: "ParameterValues",
                newName: "IX_ParameterValues_ParameterDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterValue_ConfigurationId",
                table: "ParameterValues",
                newName: "IX_ParameterValues_ConfigurationId");

            migrationBuilder.AddColumn<Guid>(
                name: "TemplateConfigurationId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParameterValues",
                table: "ParameterValues",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_TemplateConfigurationId",
                table: "Configurations",
                column: "TemplateConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_Configurations_TemplateConfigurationId",
                table: "Configurations",
                column: "TemplateConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterValues_Configurations_ConfigurationId",
                table: "ParameterValues",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterValues_ParameterDefinitions_ParameterDefinitionId",
                table: "ParameterValues",
                column: "ParameterDefinitionId",
                principalTable: "ParameterDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_Configurations_TemplateConfigurationId",
                table: "Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterValues_Configurations_ConfigurationId",
                table: "ParameterValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterValues_ParameterDefinitions_ParameterDefinitionId",
                table: "ParameterValues");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_TemplateConfigurationId",
                table: "Configurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParameterValues",
                table: "ParameterValues");

            migrationBuilder.DropColumn(
                name: "TemplateConfigurationId",
                table: "Configurations");

            migrationBuilder.RenameTable(
                name: "ParameterValues",
                newName: "ParameterValue");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterValues_ParameterDefinitionId",
                table: "ParameterValue",
                newName: "IX_ParameterValue_ParameterDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterValues_ConfigurationId",
                table: "ParameterValue",
                newName: "IX_ParameterValue_ConfigurationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParameterValue",
                table: "ParameterValue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterValue_Configurations_ConfigurationId",
                table: "ParameterValue",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterValue_ParameterDefinitions_ParameterDefinitionId",
                table: "ParameterValue",
                column: "ParameterDefinitionId",
                principalTable: "ParameterDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
