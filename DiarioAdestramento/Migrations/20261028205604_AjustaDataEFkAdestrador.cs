using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioAdestramento.Migrations
{
    /// <inheritdoc />
    public partial class AjustaDataEFkAdestrador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "SessoesTreino",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_Locais_AdestradorId",
                table: "Locais",
                column: "AdestradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cachorros_AdestradorId",
                table: "Cachorros",
                column: "AdestradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cachorros_AspNetUsers_AdestradorId",
                table: "Cachorros",
                column: "AdestradorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locais_AspNetUsers_AdestradorId",
                table: "Locais",
                column: "AdestradorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cachorros_AspNetUsers_AdestradorId",
                table: "Cachorros");

            migrationBuilder.DropForeignKey(
                name: "FK_Locais_AspNetUsers_AdestradorId",
                table: "Locais");

            migrationBuilder.DropIndex(
                name: "IX_Locais_AdestradorId",
                table: "Locais");

            migrationBuilder.DropIndex(
                name: "IX_Cachorros_AdestradorId",
                table: "Cachorros");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "SessoesTreino",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
