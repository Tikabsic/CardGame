using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruct.Migrations
{
    /// <inheritdoc />
    public partial class Room_Config_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Rooms_RoomId",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stacks_Rooms_RoomId",
                table: "Stacks");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Rooms_RoomId",
                table: "Decks",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stacks_Rooms_RoomId",
                table: "Stacks",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Rooms_RoomId",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stacks_Rooms_RoomId",
                table: "Stacks");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Rooms_RoomId",
                table: "Decks",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stacks_Rooms_RoomId",
                table: "Stacks",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId");
        }
    }
}
