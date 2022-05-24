using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class componentdefinitionid_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_ComponentDefinitions_ComponentDefinitionId",
                table: "Configurations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ComponentDefinitionId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_ComponentDefinitions_ComponentDefinitionId",
                table: "Configurations",
                column: "ComponentDefinitionId",
                principalTable: "ComponentDefinitions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_ComponentDefinitions_ComponentDefinitionId",
                table: "Configurations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ComponentDefinitionId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_ComponentDefinitions_ComponentDefinitionId",
                table: "Configurations",
                column: "ComponentDefinitionId",
                principalTable: "ComponentDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
