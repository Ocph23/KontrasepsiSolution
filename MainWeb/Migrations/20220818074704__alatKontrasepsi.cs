using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainWeb.Migrations
{
    public partial class _alatKontrasepsi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelayanan_Petugas_PetugasId",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "AlatKontrasepsiPilihan",
                table: "Pelayanan");

            migrationBuilder.AlterColumn<int>(
                name: "PetugasId",
                table: "Pelayanan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AlatKontrasepsiPilihanId",
                table: "Pelayanan",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlatKontrasepsi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nama = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Keterangan = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlatKontrasepsi", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Pelayanan_AlatKontrasepsiPilihanId",
                table: "Pelayanan",
                column: "AlatKontrasepsiPilihanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pelayanan_AlatKontrasepsi_AlatKontrasepsiPilihanId",
                table: "Pelayanan",
                column: "AlatKontrasepsiPilihanId",
                principalTable: "AlatKontrasepsi",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pelayanan_Petugas_PetugasId",
                table: "Pelayanan",
                column: "PetugasId",
                principalTable: "Petugas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelayanan_AlatKontrasepsi_AlatKontrasepsiPilihanId",
                table: "Pelayanan");

            migrationBuilder.DropForeignKey(
                name: "FK_Pelayanan_Petugas_PetugasId",
                table: "Pelayanan");

            migrationBuilder.DropTable(
                name: "AlatKontrasepsi");

            migrationBuilder.DropIndex(
                name: "IX_Pelayanan_AlatKontrasepsiPilihanId",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "AlatKontrasepsiPilihanId",
                table: "Pelayanan");

            migrationBuilder.AlterColumn<int>(
                name: "PetugasId",
                table: "Pelayanan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlatKontrasepsiPilihan",
                table: "Pelayanan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Pelayanan_Petugas_PetugasId",
                table: "Pelayanan",
                column: "PetugasId",
                principalTable: "Petugas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
