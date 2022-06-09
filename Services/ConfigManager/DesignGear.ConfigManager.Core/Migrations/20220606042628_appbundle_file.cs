using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    public partial class appbundle_file : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "AppBundles",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "AppBundles",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "AppBundles");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "AppBundles");
        }
    }
}
