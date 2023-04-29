using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruct.Migrations
{
    /// <inheritdoc />
    public partial class Minor_Db_Changes_v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStates_Players_PlayerId",
                table: "PlayerStates");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStates_Players_PlayerId",
                table: "PlayerStates",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStates_Players_PlayerId",
                table: "PlayerStates");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStates_Players_PlayerId",
                table: "PlayerStates",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
