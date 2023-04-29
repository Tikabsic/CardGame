using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruct.Migrations
{
    /// <inheritdoc />
    public partial class Minor_Db_Changes_v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "StatesId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_StatesId",
                table: "Players",
                column: "StatesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PlayerStates_StatesId",
                table: "Players",
                column: "StatesId",
                principalTable: "PlayerStates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_PlayerStates_StatesId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_StatesId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "StatesId",
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
                principalColumn: "Id");
        }
    }
}
