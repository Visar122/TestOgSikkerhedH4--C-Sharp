using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestOgSikkerhedH4.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Satus",
                table: "login",
                newName: "TwoFactorSecret");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "login",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "login");

            migrationBuilder.RenameColumn(
                name: "TwoFactorSecret",
                table: "login",
                newName: "Satus");
        }
    }
}
