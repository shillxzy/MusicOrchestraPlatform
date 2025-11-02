using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixCompositionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConcertPrograms_Compositions_CompositionId1",
                table: "ConcertPrograms");

            migrationBuilder.DropIndex(
                name: "IX_ConcertPrograms_CompositionId1",
                table: "ConcertPrograms");

            migrationBuilder.DropColumn(
                name: "CompositionId1",
                table: "ConcertPrograms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompositionId1",
                table: "ConcertPrograms",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ConcertPrograms",
                keyColumn: "Id",
                keyValue: 1,
                column: "CompositionId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConcertPrograms",
                keyColumn: "Id",
                keyValue: 2,
                column: "CompositionId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_ConcertPrograms_CompositionId1",
                table: "ConcertPrograms",
                column: "CompositionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ConcertPrograms_Compositions_CompositionId1",
                table: "ConcertPrograms",
                column: "CompositionId1",
                principalTable: "Compositions",
                principalColumn: "Id");
        }
    }
}
