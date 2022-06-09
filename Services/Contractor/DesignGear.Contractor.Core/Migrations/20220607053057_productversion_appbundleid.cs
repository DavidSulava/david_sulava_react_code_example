using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignGear.Contractor.Core.Migrations
{
    public partial class productversion_appbundleid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppBundleId",
                table: "ProductVersions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppBundleId",
                table: "ProductVersions");
        }
    }
}
