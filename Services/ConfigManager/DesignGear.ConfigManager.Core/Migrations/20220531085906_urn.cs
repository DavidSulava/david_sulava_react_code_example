using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class urn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URN",
                table: "Configurations",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_TargetFileId",
                table: "Configurations",
                column: "TargetFileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Configurations_FileItems_TargetFileId",
                table: "Configurations",
                column: "TargetFileId",
                principalTable: "FileItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configurations_FileItems_TargetFileId",
                table: "Configurations");

            migrationBuilder.DropIndex(
                name: "IX_Configurations_TargetFileId",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "URN",
                table: "Configurations");
        }
    }
}
