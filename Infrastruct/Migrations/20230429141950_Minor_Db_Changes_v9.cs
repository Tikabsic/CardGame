using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruct.Migrations
{
    /// <inheritdoc />
    public partial class Minor_Db_Changes_v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_PlayerStates_StatesId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "PlayerStates");

            migrationBuilder.DropIndex(
                name: "IX_Players_StatesId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "StatesId",
                table: "Players");

            migrationBuilder.AddColumn<bool>(
                name: "IsCardDrewFromDeck",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCardThrownToStack",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCardsDrewFromStack",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPlayerRound",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCardDrewFromDeck",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsCardThrownToStack",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsCardsDrewFromStack",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsPlayerRound",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "StatesId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlayerStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsCardDrewFromDeck = table.Column<bool>(type: "bit", nullable: false),
                    IsCardThrownToStack = table.Column<bool>(type: "bit", nullable: false),
                    IsCardsDrewFromStack = table.Column<bool>(type: "bit", nullable: false),
                    IsPlayerRound = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStates", x => x.Id);
                });

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
    }
}
