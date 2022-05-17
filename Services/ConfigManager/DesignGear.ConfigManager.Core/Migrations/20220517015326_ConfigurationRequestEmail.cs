using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class ConfigurationRequestEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentDefinitions_ComponentDefinitions_ParentComponentDefinitionId",
                table: "ComponentDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterDefinitions_ComponentDefinitions_ComponentDefinitionId",
                table: "ParameterDefinitions");

            migrationBuilder.DropTable(
                name: "ConfigurationFiles");

            migrationBuilder.DropTable(
                name: "ConfigurationInstances");

            migrationBuilder.DropTable(
                name: "ParameterValue");

            migrationBuilder.DropIndex(
                name: "IX_ComponentDefinitions_ParentComponentDefinitionId",
                table: "ComponentDefinitions");

            migrationBuilder.DropColumn(
                name: "ParentComponentDefinitionId",
                table: "ComponentDefinitions");

            migrationBuilder.RenameColumn(
                name: "ComponentDefinitionId",
                table: "ParameterDefinitions",
                newName: "ConfigurationId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterDefinitions_ComponentDefinitionId",
                table: "ParameterDefinitions",
                newName: "IX_ParameterDefinitions_ConfigurationId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ValueOptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ParameterDefinitions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "ParameterDefinitions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "ParameterDefinitions",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "AppBundleId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Configurations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "Configurations",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentConfigurationId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RootConfigurationId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TemplateConfigurationId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "Configurations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ComponentDefinitions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "ComponentDefinitions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "AppbBundles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ConfigurationRequestEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationRequestEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationRequestEmails_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationRequestParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ParameterDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationRequestParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationRequestParameters_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationRequestParameters_ParameterDefinitions_ParameterDefinitionId",
                        column: x => x.ParameterDefinitionId,
                        principalTable: "ParameterDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_AppBundleId",
                table: "Configurations",
                column: "AppBundleId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_TemplateConfigurationId",
                table: "Configurations",
                column: "TemplateConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationRequestEmails_ConfigurationId",
                table: "ConfigurationRequestEmails",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationRequestParameters_ConfigurationId",
                table: "ConfigurationRequestParameters",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationRequestParameters_ParameterDefinitionId",
                table: "ConfigurationRequestParameters",
                column: "ParameterDefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_AppbBundles_AppBundleId",
                table: "Configurations",
                column: "AppBundleId",
                principalTable: "AppbBundles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_Configurations_TemplateConfigurationId",
                table: "Configurations",
                column: "TemplateConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterDefinitions_Configurations_ConfigurationId",
                table: "ParameterDefinitions",
                column: "ConfigurationId",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_AppbBundles_AppBundleId",
                table: "Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_Configurations_TemplateConfigurationId",
                table: "Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterDefinitions_Configurations_ConfigurationId",
                table: "ParameterDefinitions");

            migrationBuilder.DropTable(
                name: "ConfigurationRequestEmails");

            migrationBuilder.DropTable(
                name: "ConfigurationRequestParameters");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_AppBundleId",
                table: "Configurations");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_TemplateConfigurationId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ValueOptions");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ParameterDefinitions");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "ParameterDefinitions");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ParameterDefinitions");

            migrationBuilder.DropColumn(
                name: "AppBundleId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "ParentConfigurationId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "RootConfigurationId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "TemplateConfigurationId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ComponentDefinitions");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "ComponentDefinitions");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "AppbBundles");

            migrationBuilder.RenameColumn(
                name: "ConfigurationId",
                table: "ParameterDefinitions",
                newName: "ComponentDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterDefinitions_ConfigurationId",
                table: "ParameterDefinitions",
                newName: "IX_ParameterDefinitions_ComponentDefinitionId");

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

            migrationBuilder.CreateTable(
                name: "ConfigurationInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    X = table.Column<int>(type: "int", nullable: false),
                    XX = table.Column<int>(type: "int", nullable: false),
                    XY = table.Column<int>(type: "int", nullable: false),
                    XZ = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    YX = table.Column<int>(type: "int", nullable: false),
                    YY = table.Column<int>(type: "int", nullable: false),
                    YZ = table.Column<int>(type: "int", nullable: false),
                    Z = table.Column<int>(type: "int", nullable: false),
                    ZX = table.Column<int>(type: "int", nullable: false),
                    ZY = table.Column<int>(type: "int", nullable: false),
                    ZZ = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationInstances_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParameterDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterValue_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParameterValue_ParameterDefinitions_ParameterDefinitionId",
                        column: x => x.ParameterDefinitionId,
                        principalTable: "ParameterDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentDefinitions_ParentComponentDefinitionId",
                table: "ComponentDefinitions",
                column: "ParentComponentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationFiles_ConfigurationId",
                table: "ConfigurationFiles",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationInstances_ConfigurationId",
                table: "ConfigurationInstances",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationInstances_ParentConfigurationId",
                table: "ConfigurationInstances",
                column: "ParentConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValue_ConfigurationId",
                table: "ParameterValue",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValue_ParameterDefinitionId",
                table: "ParameterValue",
                column: "ParameterDefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentDefinitions_ComponentDefinitions_ParentComponentDefinitionId",
                table: "ComponentDefinitions",
                column: "ParentComponentDefinitionId",
                principalTable: "ComponentDefinitions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterDefinitions_ComponentDefinitions_ComponentDefinitionId",
                table: "ParameterDefinitions",
                column: "ComponentDefinitionId",
                principalTable: "ComponentDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
