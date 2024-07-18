using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnoMarket.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoresFavotires",
                table: "Users",
                newName: "StoresFavorites");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoresFavorites",
                table: "Users",
                newName: "StoresFavotires");
        }
    }
}
