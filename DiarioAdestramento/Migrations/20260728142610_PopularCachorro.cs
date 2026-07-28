using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioAdestramento.Migrations
{
    /// <inheritdoc />
    public partial class PopularCachorro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Cachorros (Nome, Idade, Raca) VALUES ('Rex', 2, 'Pastor Alemão')");
            mb.Sql("INSERT INTO Cachorros (Nome, Idade, Raca) VALUES ('Luna', 3, 'Golden Retriever')");
            mb.Sql("INSERT INTO Cachorros (Nome, Idade, Raca) VALUES ('Thor', 1, 'Bulldog Francês')");
            mb.Sql("INSERT INTO Cachorros (Nome, Idade, Raca) VALUES ('Mel', 4, 'Labrador')");
            mb.Sql("INSERT INTO Cachorros (Nome, Idade, Raca) VALUES ('Bob', 2, 'Poodle')");
            mb.Sql("INSERT INTO Cachorros (Nome, Idade, Raca) VALUES ('Nina', 5, 'Vira-Lata')");
            mb.Sql("INSERT INTO Cachorros (Nome, Idade, Raca) VALUES ('Toby', 1, 'Shih Tzu')");
            mb.Sql("INSERT INTO Cachorros (Nome, Idade, Raca) VALUES ('Maggie', 3, 'Border Collie')");
            mb.Sql("INSERT INTO Cachorros (Nome, Idade, Raca) VALUES ('Duke', 6, 'Doberman')");
            mb.Sql("INSERT INTO Cachorros (Nome, Idade, Raca) VALUES ('Zoe', 2, 'Pinscher')");
        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Cachorros");
        }
    }
}
