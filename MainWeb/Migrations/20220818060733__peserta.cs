using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainWeb.Migrations
{
    public partial class _peserta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peserta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nama = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JenisKelamin = table.Column<int>(type: "int", nullable: false),
                    Pendidikan = table.Column<int>(type: "int", nullable: false),
                    Pekerjaan = table.Column<int>(type: "int", nullable: false),
                    TanggalLahir = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TempatLahir = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NamaPasangan = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PendidikanPasangan = table.Column<int>(type: "int", nullable: false),
                    PekerjaanPasangan = table.Column<int>(type: "int", nullable: false),
                    Alamat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peserta", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pelayanan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TerakhirHaid = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Hamil = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Menyusui = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    JumlahKehamilan = table.Column<int>(type: "int", nullable: false),
                    JumlahPersalinan = table.Column<int>(type: "int", nullable: false),
                    JumlahKeguguran = table.Column<int>(type: "int", nullable: false),
                    SakitKuning = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PendarahanPervaginam = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    KeputihanYangLama = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tumor = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Keadaan = table.Column<int>(type: "int", nullable: false),
                    BeratBadan = table.Column<double>(type: "double", nullable: false),
                    TekananDarah = table.Column<double>(type: "double", nullable: false),
                    PosisiRahim = table.Column<int>(type: "int", nullable: false),
                    AlatKontrasepsiPilihan = table.Column<int>(type: "int", nullable: false),
                    TanggalDilayani = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TanggalDicabut = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PenanggungJawab = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PesertaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelayanan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pelayanan_Peserta_PesertaId",
                        column: x => x.PesertaId,
                        principalTable: "Peserta",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KunjunganUlang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tanggal = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HaidTerakhir = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BeratBadan = table.Column<double>(type: "double", nullable: false),
                    TekananDarah = table.Column<double>(type: "double", nullable: false),
                    KompilasiBerat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Kegagalan = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PemeriksaanDanTindakan = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KonsultasiBerikut = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Datang = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PelayananId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KunjunganUlang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KunjunganUlang_Pelayanan_PelayananId",
                        column: x => x.PelayananId,
                        principalTable: "Pelayanan",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_KunjunganUlang_PelayananId",
                table: "KunjunganUlang",
                column: "PelayananId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelayanan_PesertaId",
                table: "Pelayanan",
                column: "PesertaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KunjunganUlang");

            migrationBuilder.DropTable(
                name: "Pelayanan");

            migrationBuilder.DropTable(
                name: "Peserta");
        }
    }
}
