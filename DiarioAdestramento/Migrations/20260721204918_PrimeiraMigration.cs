using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioAdestramento.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cachorros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Idade = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Raca = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cachorros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    TipoDoLocal = table.Column<int>(type: "INTEGER", nullable: true),
                    Obs = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessoesTreino",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CachorroId = table.Column<int>(type: "INTEGER", nullable: false),
                    LocalId = table.Column<int>(type: "INTEGER", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    OqueFoiTreinado = table.Column<string>(type: "TEXT", nullable: true),
                    RecomepensasUtilizadas = table.Column<string>(type: "TEXT", nullable: true),
                    TempoResposta = table.Column<int>(type: "INTEGER", nullable: true),
                    Obs = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessoesTreino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessoesTreino_Cachorros_CachorroId",
                        column: x => x.CachorroId,
                        principalTable: "Cachorros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessoesTreino_Locais_LocalId",
                        column: x => x.LocalId,
                        principalTable: "Locais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosClima",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SessaoTreinoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Momento = table.Column<int>(type: "INTEGER", nullable: true),
                    TemperaturaCelsius = table.Column<double>(type: "REAL", nullable: false),
                    CondicaoTempo = table.Column<string>(type: "TEXT", nullable: true),
                    Precipitacao = table.Column<double>(type: "REAL", nullable: true),
                    VelocidadeDeVento = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosClima", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosClima_SessoesTreino_SessaoTreinoId",
                        column: x => x.SessaoTreinoId,
                        principalTable: "SessoesTreino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosClima_SessaoTreinoId",
                table: "RegistrosClima",
                column: "SessaoTreinoId");

            migrationBuilder.CreateIndex(
                name: "IX_SessoesTreino_CachorroId",
                table: "SessoesTreino",
                column: "CachorroId");

            migrationBuilder.CreateIndex(
                name: "IX_SessoesTreino_LocalId",
                table: "SessoesTreino",
                column: "LocalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosClima");

            migrationBuilder.DropTable(
                name: "SessoesTreino");

            migrationBuilder.DropTable(
                name: "Cachorros");

            migrationBuilder.DropTable(
                name: "Locais");
        }
    }
}
