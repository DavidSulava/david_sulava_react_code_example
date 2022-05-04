using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.Contractor.Core.Migrations
{
    public partial class Configuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterDefinitions_ProductVersions_ProductVersionId",
                table: "ParameterDefinitions");

            migrationBuilder.RenameColumn(
                name: "ProductVersionId",
                table: "ParameterDefinitions",
                newName: "ConfigurationId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterDefinitions_ProductVersionId",
                table: "ParameterDefinitions",
                newName: "IX_ParameterDefinitions_ConfigurationId");

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelState = table.Column<int>(type: "int", nullable: false),
                    ProductVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_ProductVersions_ProductVersionId",
                        column: x => x.ProductVersionId,
                        principalTable: "ProductVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ProductVersionId",
                table: "Configurations",
                column: "ProductVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterDefinitions_Configurations_ConfigurationId",
                table: "ParameterDefinitions",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterDefinitions_Configurations_ConfigurationId",
                table: "ParameterDefinitions");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.RenameColumn(
                name: "ConfigurationId",
                table: "ParameterDefinitions",
                newName: "ProductVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterDefinitions_ConfigurationId",
                table: "ParameterDefinitions",
                newName: "IX_ParameterDefinitions_ProductVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterDefinitions_ProductVersions_ProductVersionId",
                table: "ParameterDefinitions",
                column: "ProductVersionId",
                principalTable: "ProductVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
