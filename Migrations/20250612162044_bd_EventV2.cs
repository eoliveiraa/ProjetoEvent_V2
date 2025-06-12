using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.event_.Migrations
{
    /// <inheritdoc />
    public partial class bd_EventV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PresencasEventos_IdEvento",
                table: "PresencasEventos");

            migrationBuilder.CreateIndex(
                name: "IX_PresencasEventos_IdEvento",
                table: "PresencasEventos",
                column: "IdEvento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PresencasEventos_IdEvento",
                table: "PresencasEventos");

            migrationBuilder.CreateIndex(
                name: "IX_PresencasEventos_IdEvento",
                table: "PresencasEventos",
                column: "IdEvento",
                unique: true);
        }
    }
}
