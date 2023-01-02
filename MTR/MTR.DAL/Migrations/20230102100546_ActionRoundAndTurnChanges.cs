using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ActionRoundAndTurnChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TurnCards_TurnCards_OppositeCardId",
                table: "TurnCards");

            migrationBuilder.RenameColumn(
                name: "OppositeCardId",
                table: "TurnCards",
                newName: "OppositeTurnCardId");

            migrationBuilder.RenameIndex(
                name: "IX_TurnCards_OppositeCardId",
                table: "TurnCards",
                newName: "IX_TurnCards_OppositeTurnCardId");

            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "MuckedCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MuckedCards_ActionId",
                table: "MuckedCards",
                column: "ActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MuckedCards_Actions_ActionId",
                table: "MuckedCards",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurnCards_TurnCards_OppositeTurnCardId",
                table: "TurnCards",
                column: "OppositeTurnCardId",
                principalTable: "TurnCards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MuckedCards_Actions_ActionId",
                table: "MuckedCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TurnCards_TurnCards_OppositeTurnCardId",
                table: "TurnCards");

            migrationBuilder.DropIndex(
                name: "IX_MuckedCards_ActionId",
                table: "MuckedCards");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "MuckedCards");

            migrationBuilder.RenameColumn(
                name: "OppositeTurnCardId",
                table: "TurnCards",
                newName: "OppositeCardId");

            migrationBuilder.RenameIndex(
                name: "IX_TurnCards_OppositeTurnCardId",
                table: "TurnCards",
                newName: "IX_TurnCards_OppositeCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_TurnCards_TurnCards_OppositeCardId",
                table: "TurnCards",
                column: "OppositeCardId",
                principalTable: "TurnCards",
                principalColumn: "Id");
        }
    }
}
