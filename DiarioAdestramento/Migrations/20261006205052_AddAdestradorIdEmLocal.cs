using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioAdestramento.Migrations
{
    /// <inheritdoc />
    public partial class AddAdestradorIdEmLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdestradorId",
                table: "Locais",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdestradorId",
                table: "Locais");
        }
    }
}
