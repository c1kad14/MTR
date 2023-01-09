using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RoundPositionsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartPosition",
                table: "Rounds");

            migrationBuilder.CreateTable(
                name: "PlayerRoundPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoundId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRoundPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerRoundPosition_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerRoundPosition_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRoundPosition_PlayerId",
                table: "PlayerRoundPosition",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRoundPosition_RoundId",
                table: "PlayerRoundPosition",
                column: "RoundId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerRoundPosition");

            migrationBuilder.AddColumn<int>(
                name: "StartPosition",
                table: "Rounds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
