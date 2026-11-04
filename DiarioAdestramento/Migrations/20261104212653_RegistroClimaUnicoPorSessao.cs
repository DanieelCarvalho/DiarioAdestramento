using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioAdestramento.Migrations
{
    /// <inheritdoc />
    public partial class RegistroClimaUnicoPorSessao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Mantém só o primeiro registro (o do início) de cada sessão que tinha dois,
            // senão o índice único falha.
            migrationBuilder.Sql("""
    DELETE FROM "RegistrosClima" r
    USING "RegistrosClima" outro
    WHERE r."SessaoTreinoId" = outro."SessaoTreinoId"
      AND r."Id" > outro."Id";
    """);
            migrationBuilder.DropIndex(
                name: "IX_RegistrosClima_SessaoTreinoId",
                table: "RegistrosClima");

            migrationBuilder.DropColumn(
                name: "Momento",
                table: "RegistrosClima");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosClima_SessaoTreinoId",
                table: "RegistrosClima",
                column: "SessaoTreinoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RegistrosClima_SessaoTreinoId",
                table: "RegistrosClima");

            migrationBuilder.AddColumn<int>(
                name: "Momento",
                table: "RegistrosClima",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosClima_SessaoTreinoId",
                table: "RegistrosClima",
                column: "SessaoTreinoId");
        }
    }
}
