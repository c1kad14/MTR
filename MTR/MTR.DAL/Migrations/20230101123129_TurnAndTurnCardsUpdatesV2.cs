using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TurnAndTurnCardsUpdatesV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheats_Actions_ActionId",
                table: "Cheats");

            migrationBuilder.DropForeignKey(
                name: "FK_Cheats_TurnCards_CardId",
                table: "Cheats");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TurnCards_Guid",
                table: "TurnCards");

            migrationBuilder.DropIndex(
                name: "IX_Cheats_ActionId",
                table: "Cheats");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "TurnCards");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "Cheats");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Cheats",
                newName: "TurnCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Cheats_CardId",
                table: "Cheats",
                newName: "IX_Cheats_TurnCardId");

            migrationBuilder.AddColumn<int>(
                name: "TurnId",
                table: "TurnCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "RoundCards",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RoundCards_Guid",
                table: "RoundCards",
                column: "Guid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Players_Guid",
                table: "Players",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_TurnCards_TurnId",
                table: "TurnCards",
                column: "TurnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cheats_TurnCards_TurnCardId",
                table: "Cheats",
                column: "TurnCardId",
                principalTable: "TurnCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurnCards_Turns_TurnId",
                table: "TurnCards",
                column: "TurnId",
                principalTable: "Turns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheats_TurnCards_TurnCardId",
                table: "Cheats");

            migrationBuilder.DropForeignKey(
                name: "FK_TurnCards_Turns_TurnId",
                table: "TurnCards");

            migrationBuilder.DropIndex(
                name: "IX_TurnCards_TurnId",
                table: "TurnCards");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_RoundCards_Guid",
                table: "RoundCards");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Players_Guid",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TurnId",
                table: "TurnCards");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "RoundCards");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "TurnCardId",
                table: "Cheats",
                newName: "CardId");

            migrationBuilder.RenameIndex(
                name: "IX_Cheats_TurnCardId",
                table: "Cheats",
                newName: "IX_Cheats_CardId");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "TurnCards",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "Cheats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TurnCards_Guid",
                table: "TurnCards",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_Cheats_ActionId",
                table: "Cheats",
                column: "ActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cheats_Actions_ActionId",
                table: "Cheats",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cheats_TurnCards_CardId",
                table: "Cheats",
                column: "CardId",
                principalTable: "TurnCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
