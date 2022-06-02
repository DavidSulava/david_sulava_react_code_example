using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class targetfileid_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Configurations_TargetFileId",
                table: "Configurations");

            migrationBuilder.AlterColumn<Guid>(
                name: "TargetFileId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_TargetFileId",
                table: "Configurations",
                column: "TargetFileId",
                unique: true,
                filter: "[TargetFileId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Configurations_TargetFileId",
                table: "Configurations");

            migrationBuilder.AlterColumn<Guid>(
                name: "TargetFileId",
                table: "Configurations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_TargetFileId",
                table: "Configurations",
                column: "TargetFileId",
                unique: true);
        }
    }
}
