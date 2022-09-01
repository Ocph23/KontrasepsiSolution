using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainWeb.Migrations
{
    public partial class _tahapanks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelayanan_Peserta_PesertaId",
                table: "Pelayanan");

            migrationBuilder.AddColumn<int>(
                name: "JKN",
                table: "Peserta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TahapanKS",
                table: "Peserta",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "PesertaId",
                table: "Pelayanan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pelayanan_Peserta_PesertaId",
                table: "Pelayanan",
                column: "PesertaId",
                principalTable: "Peserta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelayanan_Peserta_PesertaId",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "JKN",
                table: "Peserta");

            migrationBuilder.DropColumn(
                name: "TahapanKS",
                table: "Peserta");

            migrationBuilder.AlterColumn<int>(
                name: "PesertaId",
                table: "Pelayanan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pelayanan_Peserta_PesertaId",
                table: "Pelayanan",
                column: "PesertaId",
                principalTable: "Peserta",
                principalColumn: "Id");
        }
    }
}
