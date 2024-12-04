using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScacchiDS.Server.Migrations
{
    /// <inheritdoc />
    public partial class modificheClassiSviluppoGam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Giocatori_AspNetUsers_AspNetUserId",
                table: "Giocatori");

            migrationBuilder.DropForeignKey(
                name: "FK_Mosse_Giocatori_GiocatoreId",
                table: "Mosse");

            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Giocatori_GiocatoreBiancoId",
                table: "Partite");

            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Giocatori_GiocatoreNeroId",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Partite_GiocatoreBiancoId",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Partite_GiocatoreNeroId",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Mosse_GiocatoreId",
                table: "Mosse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Giocatori",
                table: "Giocatori");

            migrationBuilder.DropIndex(
                name: "IX_Giocatori_AspNetUserId",
                table: "Giocatori");

            migrationBuilder.DropColumn(
                name: "GiocatoreBiancoId",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "GiocatoreNeroId",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "GiocatoreId",
                table: "Mosse");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Giocatori");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Giocatori");

            migrationBuilder.RenameColumn(
                name: "DataRegistrazione",
                table: "Giocatori",
                newName: "DataOraCreazione");

            migrationBuilder.AddColumn<string>(
                name: "GiocatoreBiancoSessionId",
                table: "Partite",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GiocatoreNeroSessionId",
                table: "Partite",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GiocatoreSessionId",
                table: "Mosse",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AspNetUserId",
                table: "Giocatori",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "sessionId",
                table: "Giocatori",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Giocatori",
                table: "Giocatori",
                column: "sessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Partite_GiocatoreBiancoSessionId",
                table: "Partite",
                column: "GiocatoreBiancoSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Partite_GiocatoreNeroSessionId",
                table: "Partite",
                column: "GiocatoreNeroSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Mosse_GiocatoreSessionId",
                table: "Mosse",
                column: "GiocatoreSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Giocatori_AspNetUserId",
                table: "Giocatori",
                column: "AspNetUserId",
                unique: true,
                filter: "[AspNetUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Giocatori_AspNetUsers_AspNetUserId",
                table: "Giocatori",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mosse_Giocatori_GiocatoreSessionId",
                table: "Mosse",
                column: "GiocatoreSessionId",
                principalTable: "Giocatori",
                principalColumn: "sessionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Giocatori_GiocatoreBiancoSessionId",
                table: "Partite",
                column: "GiocatoreBiancoSessionId",
                principalTable: "Giocatori",
                principalColumn: "sessionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Giocatori_GiocatoreNeroSessionId",
                table: "Partite",
                column: "GiocatoreNeroSessionId",
                principalTable: "Giocatori",
                principalColumn: "sessionId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Giocatori_AspNetUsers_AspNetUserId",
                table: "Giocatori");

            migrationBuilder.DropForeignKey(
                name: "FK_Mosse_Giocatori_GiocatoreSessionId",
                table: "Mosse");

            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Giocatori_GiocatoreBiancoSessionId",
                table: "Partite");

            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Giocatori_GiocatoreNeroSessionId",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Partite_GiocatoreBiancoSessionId",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Partite_GiocatoreNeroSessionId",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Mosse_GiocatoreSessionId",
                table: "Mosse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Giocatori",
                table: "Giocatori");

            migrationBuilder.DropIndex(
                name: "IX_Giocatori_AspNetUserId",
                table: "Giocatori");

            migrationBuilder.DropColumn(
                name: "GiocatoreBiancoSessionId",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "GiocatoreNeroSessionId",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "GiocatoreSessionId",
                table: "Mosse");

            migrationBuilder.DropColumn(
                name: "sessionId",
                table: "Giocatori");

            migrationBuilder.RenameColumn(
                name: "DataOraCreazione",
                table: "Giocatori",
                newName: "DataRegistrazione");

            migrationBuilder.AddColumn<int>(
                name: "GiocatoreBiancoId",
                table: "Partite",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GiocatoreNeroId",
                table: "Partite",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GiocatoreId",
                table: "Mosse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AspNetUserId",
                table: "Giocatori",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Giocatori",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Giocatori",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Giocatori",
                table: "Giocatori",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Partite_GiocatoreBiancoId",
                table: "Partite",
                column: "GiocatoreBiancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partite_GiocatoreNeroId",
                table: "Partite",
                column: "GiocatoreNeroId");

            migrationBuilder.CreateIndex(
                name: "IX_Mosse_GiocatoreId",
                table: "Mosse",
                column: "GiocatoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Giocatori_AspNetUserId",
                table: "Giocatori",
                column: "AspNetUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Giocatori_AspNetUsers_AspNetUserId",
                table: "Giocatori",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mosse_Giocatori_GiocatoreId",
                table: "Mosse",
                column: "GiocatoreId",
                principalTable: "Giocatori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Giocatori_GiocatoreBiancoId",
                table: "Partite",
                column: "GiocatoreBiancoId",
                principalTable: "Giocatori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Giocatori_GiocatoreNeroId",
                table: "Partite",
                column: "GiocatoreNeroId",
                principalTable: "Giocatori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
