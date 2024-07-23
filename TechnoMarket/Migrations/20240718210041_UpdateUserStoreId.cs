using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnoMarket.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserStoreId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Users");
        }
    }
}
