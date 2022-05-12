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
                    InventorVersion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppbBundles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TemplateConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelState = table.Column<int>(type: "int", nullable: false),
                    TargetFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "ParameterDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayPriority = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ValueType = table.Column<int>(type: "int", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ComponentDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    AllowCustomValues = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterDefinitions_ComponentDefinitions_ComponentDefinitionId",
                        column: x => x.ComponentDefinitionId,
                        principalTable: "ComponentDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    Z = table.Column<int>(type: "int", nullable: false),
                    XX = table.Column<int>(type: "int", nullable: false),
                    YY = table.Column<int>(type: "int", nullable: false),
                    ZZ = table.Column<int>(type: "int", nullable: false),
                    XY = table.Column<int>(type: "int", nullable: false),
                    YX = table.Column<int>(type: "int", nullable: false),
                    XZ = table.Column<int>(type: "int", nullable: false),
                    ZX = table.Column<int>(type: "int", nullable: false),
                    YZ = table.Column<int>(type: "int", nullable: false),
                    ZY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationInstances_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationInstances_Configurations_ParentConfigurationId",
                        column: x => x.ParentConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ParameterValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ParameterDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterValue_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParameterValue_ParameterDefinitions_ParameterDefinitionId",
                        column: x => x.ParameterDefinitionId,
                        principalTable: "ParameterDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValueOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "IX_ConfigurationInstances_ConfigurationId",
                table: "ConfigurationInstances",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationInstances_ParentConfigurationId",
                table: "ConfigurationInstances",
                column: "ParentConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ComponentDefinitionId",
                table: "Configurations",
                column: "ComponentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterDefinitions_ComponentDefinitionId",
                table: "ParameterDefinitions",
                column: "ComponentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValue_ConfigurationId",
                table: "ParameterValue",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValue_ParameterDefinitionId",
                table: "ParameterValue",
                column: "ParameterDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueOptions_ParameterDefinitionId",
                table: "ValueOptions",
                column: "ParameterDefinitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppbBundles");

            migrationBuilder.DropTable(
                name: "ConfigurationInstances");

            migrationBuilder.DropTable(
                name: "ParameterValue");

            migrationBuilder.DropTable(
                name: "ValueOptions");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "ParameterDefinitions");

            migrationBuilder.DropTable(
                name: "ComponentDefinitions");
        }
    }
}
