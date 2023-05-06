using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruct.Migrations
{
    /// <inheritdoc />
    public partial class Minor_Changes_v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListIndex",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListIndex",
                table: "Players");
        }
    }
}
