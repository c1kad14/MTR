using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RoundCardsChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_RoundResults_Guid",
                table: "RoundResults");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_RoundCards_Guid",
                table: "RoundCards");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cheats_Guid",
                table: "Cheats");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "RoundResults");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "RoundCards");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Cheats");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "UserDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserDetails_Guid",
                table: "UserDetails",
                column: "Guid");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Suit",
                value: "SPADES");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Suit",
                value: "SPADES");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Suit",
                value: "SPADES");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 4,
                column: "Suit",
                value: "SPADES");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 5,
                column: "Suit",
                value: "SPADES");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 6,
                column: "Suit",
                value: "SPADES");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 7,
                column: "Suit",
                value: "SPADES");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 8,
                column: "Suit",
                value: "SPADES");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 9,
                column: "Suit",
                value: "SPADES");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 10,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 11,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 12,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 13,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 14,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 15,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 16,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 17,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 18,
                column: "Suit",
                value: "HEARTS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserDetails_Guid",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "UserDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "RoundResults",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "RoundCards",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Cheats",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RoundResults_Guid",
                table: "RoundResults",
                column: "Guid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RoundCards_Guid",
                table: "RoundCards",
                column: "Guid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cheats_Guid",
                table: "Cheats",
                column: "Guid");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 4,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 5,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 6,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 7,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 8,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 9,
                column: "Suit",
                value: "HEARTS");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 10,
                column: "Suit",
                value: "SPADED");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 11,
                column: "Suit",
                value: "SPADED");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 12,
                column: "Suit",
                value: "SPADED");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 13,
                column: "Suit",
                value: "SPADED");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 14,
                column: "Suit",
                value: "SPADED");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 15,
                column: "Suit",
                value: "SPADED");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 16,
                column: "Suit",
                value: "SPADED");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 17,
                column: "Suit",
                value: "SPADED");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 18,
                column: "Suit",
                value: "SPADED");
        }
    }
}
