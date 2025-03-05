using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PL1_G1.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Dt_Nasc",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Morada",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dt_Nasc",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Morada",
                table: "User");
        }
    }
}
