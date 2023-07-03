using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Przychodnia.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Refundacja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rodzaj = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lekarze",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KosztWizyty = table.Column<int>(type: "int", nullable: false),
                    Specjalizacja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UzytkownikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekarze", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lekarze_Uzytkownicy_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pacjenci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrzyjmowaneLeki = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alergie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miejscowosc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodPocztowy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UzytkownikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacjenci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacjenci_Uzytkownicy_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Terminy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LekarzId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminy_Lekarze_LekarzId",
                        column: x => x.LekarzId,
                        principalTable: "Lekarze",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recepty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataWystawienia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KodRecepty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wystawiajacy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zalecenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dawkowanie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacjentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recepty_Pacjenci_PacjentId",
                        column: x => x.PacjentId,
                        principalTable: "Pacjenci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wizyty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacjentId = table.Column<int>(type: "int", nullable: false),
                    TerminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wizyty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wizyty_Pacjenci_PacjentId",
                        column: x => x.PacjentId,
                        principalTable: "Pacjenci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wizyty_Terminy_TerminId",
                        column: x => x.TerminId,
                        principalTable: "Terminy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LekiRecepty",
                columns: table => new
                {
                    LekId = table.Column<int>(type: "int", nullable: false),
                    ReceptaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LekiRecepty", x => new { x.LekId, x.ReceptaId });
                    table.ForeignKey(
                        name: "FK_LekiRecepty_Leki_LekId",
                        column: x => x.LekId,
                        principalTable: "Leki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LekiRecepty_Recepty_ReceptaId",
                        column: x => x.ReceptaId,
                        principalTable: "Recepty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lekarze_UzytkownikId",
                table: "Lekarze",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_LekiRecepty_ReceptaId",
                table: "LekiRecepty",
                column: "ReceptaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacjenci_UzytkownikId",
                table: "Pacjenci",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Recepty_PacjentId",
                table: "Recepty",
                column: "PacjentId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminy_LekarzId",
                table: "Terminy",
                column: "LekarzId");

            migrationBuilder.CreateIndex(
                name: "IX_Wizyty_PacjentId",
                table: "Wizyty",
                column: "PacjentId");

            migrationBuilder.CreateIndex(
                name: "IX_Wizyty_TerminId",
                table: "Wizyty",
                column: "TerminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LekiRecepty");

            migrationBuilder.DropTable(
                name: "Wizyty");

            migrationBuilder.DropTable(
                name: "Leki");

            migrationBuilder.DropTable(
                name: "Recepty");

            migrationBuilder.DropTable(
                name: "Terminy");

            migrationBuilder.DropTable(
                name: "Pacjenci");

            migrationBuilder.DropTable(
                name: "Lekarze");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");
        }
    }
}
