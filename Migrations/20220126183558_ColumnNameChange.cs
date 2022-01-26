using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtTokenApi.Migrations
{
    public partial class ColumnNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenExpiredOn",
                table: "RefreshTokens",
                newName: "TokenExpireOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenExpireOn",
                table: "RefreshTokens",
                newName: "TokenExpiredOn");
        }
    }
}
