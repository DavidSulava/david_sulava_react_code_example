using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppbBundles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesignGearVersion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    InventorVersion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppbBundles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    Z = table.Column<double>(type: "float", nullable: false),
                    XX = table.Column<double>(type: "float", nullable: false),
                    YY = table.Column<double>(type: "float", nullable: false),
                    ZZ = table.Column<double>(type: "float", nullable: false),
                    XY = table.Column<double>(type: "float", nullable: false),
                    YX = table.Column<double>(type: "float", nullable: false),
                    XZ = table.Column<double>(type: "float", nullable: false),
                    ZX = table.Column<double>(type: "float", nullable: false),
                    YZ = table.Column<double>(type: "float", nullable: false),
                    ZY = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationInstances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TemplateConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppBundleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentDefinitions_AppbBundles_AppBundleId",
                        column: x => x.AppBundleId,
                        principalTable: "AppbBundles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SvfStatus = table.Column<int>(type: "int", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelState = table.Column<int>(type: "int", nullable: false),
                    TargetFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RootConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TemplateConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_ComponentDefinitions_ComponentDefinitionId",
                        column: x => x.ComponentDefinitionId,
                        principalTable: "ComponentDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Configurations_Configurations_TemplateConfigurationId",
                        column: x => x.TemplateConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ParameterDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayPriority = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ValueType = table.Column<int>(type: "int", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    AllowCustomValues = table.Column<bool>(type: "bit", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterDefinitions_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValueOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueOptions_ParameterDefinitions_ParameterDefinitionId",
                        column: x => x.ParameterDefinitionId,
                        principalTable: "ParameterDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentDefinitions_AppBundleId",
                table: "ComponentDefinitions",
                column: "AppBundleId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ComponentDefinitionId",
                table: "Configurations",
                column: "ComponentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_TemplateConfigurationId",
                table: "Configurations",
                column: "TemplateConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterDefinitions_ConfigurationId",
                table: "ParameterDefinitions",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueOptions_ParameterDefinitionId",
                table: "ValueOptions",
                column: "ParameterDefinitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationInstances");

            migrationBuilder.DropTable(
                name: "ValueOptions");

            migrationBuilder.DropTable(
                name: "ParameterDefinitions");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "ComponentDefinitions");

            migrationBuilder.DropTable(
                name: "AppbBundles");
        }
    }
}
