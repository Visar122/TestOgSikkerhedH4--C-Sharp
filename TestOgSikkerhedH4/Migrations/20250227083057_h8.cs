using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestOgSikkerhedH4.Migrations
{
    /// <inheritdoc />
    public partial class h8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoFactorSecret",
                table: "login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TwoFactorSecret",
                table: "login",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
