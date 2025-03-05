using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PL1_G1_1.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Morada",
                table: "Autenticado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "dtNasc",
                table: "Autenticado",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Morada",
                table: "Autenticado");

            migrationBuilder.DropColumn(
                name: "dtNasc",
                table: "Autenticado");
        }
    }
}
