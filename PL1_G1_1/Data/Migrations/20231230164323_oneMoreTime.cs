using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PL1_G1_1.Data.Migrations
{
    /// <inheritdoc />
    public partial class oneMoreTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Autenticado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Autenticado");
        }
    }
}
