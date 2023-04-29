using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruct.Migrations
{
    /// <inheritdoc />
    public partial class Minor_Db_Changes_v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_PlayerStates_StatesId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_StatesId",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "PlayerStates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStates_PlayerId",
                table: "PlayerStates",
                column: "PlayerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStates_Players_PlayerId",
                table: "PlayerStates",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStates_Players_PlayerId",
                table: "PlayerStates");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStates_PlayerId",
                table: "PlayerStates");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayerStates");

            migrationBuilder.CreateIndex(
                name: "IX_Players_StatesId",
                table: "Players",
                column: "StatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PlayerStates_StatesId",
                table: "Players",
                column: "StatesId",
                principalTable: "PlayerStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
