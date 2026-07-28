using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioAdestramento.Migrations
{
    /// <inheritdoc />
    public partial class PopularLocais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            // Locais Abertos (TipoDoLocal = 0)
            mb.Sql("INSERT INTO Locais (Nome, Latitude, Longitude, TipoDoLocal, Obs) VALUES ('Praça do Skate', -22.76003571228771, -43.4566406338364, 0, 'Muitas pessoas passando')");
            mb.Sql("INSERT INTO Locais (Nome, Latitude, Longitude, TipoDoLocal, Obs) VALUES ('Parque Ibirapuera', -23.588557, -46.658658, 0, 'Ótimo para caminhadas e brincadeiras')");
            mb.Sql("INSERT INTO Locais (Nome, Latitude, Longitude, TipoDoLocal, Obs) VALUES ('Praia do Flamengo', -22.928445, -43.177924, 0, 'Boa para socialização com outros cães')");
            mb.Sql("INSERT INTO Locais (Nome, Latitude, Longitude, TipoDoLocal, Obs) VALUES ('Campo do Jardim', -23.550520, -46.633308, 0, 'Espaço amplo para corridas')");
            mb.Sql("INSERT INTO Locais (Nome, Latitude, Longitude, TipoDoLocal, Obs) VALUES ('Bosque Municipal', -23.412204, -47.530312, 0, 'Muita natureza e sombra')");
            mb.Sql("INSERT INTO Locais (Nome, Latitude, Longitude, TipoDoLocal, Obs) VALUES ('Praça Central', -22.906847, -43.172897, 0, 'Bom para encontros com outros tutores')");

            // Locais Fechados (TipoDoLocal = 1)
            mb.Sql("INSERT INTO Locais (Nome, Latitude, Longitude, TipoDoLocal, Obs) VALUES ('Pet Shop Center', -23.563210, -46.652420, 1, 'Ambiente climatizado e seguro')");
            mb.Sql("INSERT INTO Locais (Nome, Latitude, Longitude, TipoDoLocal, Obs) VALUES ('Dog Park Indoor', -23.548572, -46.638861, 1, 'Espaço coberto com brinquedos')");
            mb.Sql("INSERT INTO Locais (Nome, Latitude, Longitude, TipoDoLocal, Obs) VALUES ('Centro de Adestramento', -23.578942, -46.711242, 1, 'Profissionais especializados')");
            mb.Sql("INSERT INTO Locais (Nome, Latitude, Longitude, TipoDoLocal, Obs) VALUES ('Hotel Pet', -22.915578, -43.218354, 1, 'Ambiente controlado e seguro')");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Locais");
        }
    }
}
