using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruct.Migrations
{
    /// <inheritdoc />
    public partial class Minor_Db_Changes_v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStates_Players_PlayerId",
                table: "PlayerStates");

            migrationBuilder.DropColumn(
                name: "StatesId",
                table: "Players");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStates_Players_PlayerId",
                table: "PlayerStates",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStates_Players_PlayerId",
                table: "PlayerStates");

            migrationBuilder.AddColumn<int>(
                name: "StatesId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStates_Players_PlayerId",
                table: "PlayerStates",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
