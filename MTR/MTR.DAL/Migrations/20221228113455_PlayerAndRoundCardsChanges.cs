using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PlayerAndRoundCardsChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCards_RoundCards_CardId",
                table: "PlayerCards");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "PlayerCards",
                newName: "RoundCardId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCards_CardId",
                table: "PlayerCards",
                newName: "IX_PlayerCards_RoundCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCards_RoundCards_RoundCardId",
                table: "PlayerCards",
                column: "RoundCardId",
                principalTable: "RoundCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCards_RoundCards_RoundCardId",
                table: "PlayerCards");

            migrationBuilder.RenameColumn(
                name: "RoundCardId",
                table: "PlayerCards",
                newName: "CardId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCards_RoundCardId",
                table: "PlayerCards",
                newName: "IX_PlayerCards_CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCards_RoundCards_CardId",
                table: "PlayerCards",
                column: "CardId",
                principalTable: "RoundCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
