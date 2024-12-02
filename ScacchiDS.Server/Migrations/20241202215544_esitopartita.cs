using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScacchiDS.Server.Migrations
{
    /// <inheritdoc />
    public partial class esitopartita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partite_EsitiPartita_EsitoId",
                table: "Partite");

            migrationBuilder.RenameColumn(
                name: "EsitoId",
                table: "Partite",
                newName: "EsitoPartitaId");

            migrationBuilder.RenameIndex(
                name: "IX_Partite_EsitoId",
                table: "Partite",
                newName: "IX_Partite_EsitoPartitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_EsitiPartita_EsitoPartitaId",
                table: "Partite",
                column: "EsitoPartitaId",
                principalTable: "EsitiPartita",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partite_EsitiPartita_EsitoPartitaId",
                table: "Partite");

            migrationBuilder.RenameColumn(
                name: "EsitoPartitaId",
                table: "Partite",
                newName: "EsitoId");

            migrationBuilder.RenameIndex(
                name: "IX_Partite_EsitoPartitaId",
                table: "Partite",
                newName: "IX_Partite_EsitoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_EsitiPartita_EsitoId",
                table: "Partite",
                column: "EsitoId",
                principalTable: "EsitiPartita",
                principalColumn: "Id");
        }
    }
}
