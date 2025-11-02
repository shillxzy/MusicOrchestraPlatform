using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CatalogService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Duration = table.Column<double>(type: "double precision", nullable: false),
                    Genre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                    table.CheckConstraint("CK_Instrument_Price", "\"Price\" > 0");
                });

            migrationBuilder.CreateTable(
                name: "ConcertPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConcertId = table.Column<int>(type: "integer", nullable: false),
                    CompositionId = table.Column<int>(type: "integer", nullable: false),
                    CompositionId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcertPrograms_Compositions_CompositionId",
                        column: x => x.CompositionId,
                        principalTable: "Compositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConcertPrograms_Compositions_CompositionId1",
                        column: x => x.CompositionId1,
                        principalTable: "Compositions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstrumentImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InstrumentId = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentImages_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Performers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    InstrumentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performers_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Compositions",
                columns: new[] { "Id", "Duration", "Genre", "Title" },
                values: new object[,]
                {
                    { 1, 3600.0, "Classical", "Symphony No. 5" },
                    { 2, 1800.0, "Jazz", "Jazz Improvisation" }
                });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "Violin", 1200m, "String" },
                    { 2, "Trumpet", 900m, "Brass" },
                    { 3, "Drum", 1500m, "Percussion" }
                });

            migrationBuilder.InsertData(
                table: "ConcertPrograms",
                columns: new[] { "Id", "CompositionId", "CompositionId1", "ConcertId" },
                values: new object[,]
                {
                    { 1, 1, null, 1 },
                    { 2, 2, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "InstrumentImages",
                columns: new[] { "Id", "InstrumentId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "violin.jpg" },
                    { 2, 2, "trumpet.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Performers",
                columns: new[] { "Id", "InstrumentId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "John Doe" },
                    { 2, 2, "Maria Smith" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConcertPrograms_CompositionId",
                table: "ConcertPrograms",
                column: "CompositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertPrograms_CompositionId1",
                table: "ConcertPrograms",
                column: "CompositionId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertPrograms_ConcertId_CompositionId",
                table: "ConcertPrograms",
                columns: new[] { "ConcertId", "CompositionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentImages_InstrumentId",
                table: "InstrumentImages",
                column: "InstrumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Performers_InstrumentId",
                table: "Performers",
                column: "InstrumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConcertPrograms");

            migrationBuilder.DropTable(
                name: "InstrumentImages");

            migrationBuilder.DropTable(
                name: "Performers");

            migrationBuilder.DropTable(
                name: "Compositions");

            migrationBuilder.DropTable(
                name: "Instruments");
        }
    }
}
