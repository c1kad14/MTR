using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RoundReadyModifiedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "RoundReady",
                newName: "Modified");

            migrationBuilder.AddColumn<bool>(
                name: "Ready",
                table: "RoundReady",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ready",
                table: "RoundReady");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "RoundReady",
                newName: "Guid");
        }
    }
}
