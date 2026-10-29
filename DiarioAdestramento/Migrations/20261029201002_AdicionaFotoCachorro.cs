using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioAdestramento.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaFotoCachorro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Cachorros",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Cachorros");
        }
    }
}
