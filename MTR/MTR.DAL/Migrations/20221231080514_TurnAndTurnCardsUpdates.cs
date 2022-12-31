using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TurnAndTurnCardsUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MuckedCards_RoundCards_CardId",
                table: "MuckedCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TurnCards_RoundCards_CardId",
                table: "TurnCards");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "TurnCards",
                newName: "RoundCardId");

            migrationBuilder.RenameIndex(
                name: "IX_TurnCards_CardId",
                table: "TurnCards",
                newName: "IX_TurnCards_RoundCardId");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "MuckedCards",
                newName: "RoundCardId");

            migrationBuilder.RenameIndex(
                name: "IX_MuckedCards_CardId",
                table: "MuckedCards",
                newName: "IX_MuckedCards_RoundCardId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Turns",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Actions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_MuckedCards_RoundCards_RoundCardId",
                table: "MuckedCards",
                column: "RoundCardId",
                principalTable: "RoundCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurnCards_RoundCards_RoundCardId",
                table: "TurnCards",
                column: "RoundCardId",
                principalTable: "RoundCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MuckedCards_RoundCards_RoundCardId",
                table: "MuckedCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TurnCards_RoundCards_RoundCardId",
                table: "TurnCards");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Turns");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "RoundCardId",
                table: "TurnCards",
                newName: "CardId");

            migrationBuilder.RenameIndex(
                name: "IX_TurnCards_RoundCardId",
                table: "TurnCards",
                newName: "IX_TurnCards_CardId");

            migrationBuilder.RenameColumn(
                name: "RoundCardId",
                table: "MuckedCards",
                newName: "CardId");

            migrationBuilder.RenameIndex(
                name: "IX_MuckedCards_RoundCardId",
                table: "MuckedCards",
                newName: "IX_MuckedCards_CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_MuckedCards_RoundCards_CardId",
                table: "MuckedCards",
                column: "CardId",
                principalTable: "RoundCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurnCards_RoundCards_CardId",
                table: "TurnCards",
                column: "CardId",
                principalTable: "RoundCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
