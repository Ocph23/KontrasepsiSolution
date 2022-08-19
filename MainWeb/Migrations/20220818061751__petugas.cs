using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainWeb.Migrations
{
    public partial class _petugas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pelayanan");

            migrationBuilder.AddColumn<int>(
                name: "PetugasId",
                table: "Pelayanan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PetugasId",
                table: "KunjunganUlang",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Petugas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nama = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Jabatan = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Alamat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telepon = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Petugas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Pelayanan_PetugasId",
                table: "Pelayanan",
                column: "PetugasId");

            migrationBuilder.CreateIndex(
                name: "IX_KunjunganUlang_PetugasId",
                table: "KunjunganUlang",
                column: "PetugasId");

            migrationBuilder.AddForeignKey(
                name: "FK_KunjunganUlang_Petugas_PetugasId",
                table: "KunjunganUlang",
                column: "PetugasId",
                principalTable: "Petugas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pelayanan_Petugas_PetugasId",
                table: "Pelayanan",
                column: "PetugasId",
                principalTable: "Petugas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KunjunganUlang_Petugas_PetugasId",
                table: "KunjunganUlang");

            migrationBuilder.DropForeignKey(
                name: "FK_Pelayanan_Petugas_PetugasId",
                table: "Pelayanan");

            migrationBuilder.DropTable(
                name: "Petugas");

            migrationBuilder.DropIndex(
                name: "IX_Pelayanan_PetugasId",
                table: "Pelayanan");

            migrationBuilder.DropIndex(
                name: "IX_KunjunganUlang_PetugasId",
                table: "KunjunganUlang");

            migrationBuilder.DropColumn(
                name: "PetugasId",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "PetugasId",
                table: "KunjunganUlang");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pelayanan",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
