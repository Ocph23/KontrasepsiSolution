using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainWeb.Migrations
{
    public partial class _anakhidup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnakHidupLaki",
                table: "Pelayanan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnakHidupPerempuan",
                table: "Pelayanan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CaraKBTerakhirId",
                table: "Pelayanan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "StatusKB",
                table: "Pelayanan",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UmurAnakTerkecilBulan",
                table: "Pelayanan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UmurAnakTerkecilTahun",
                table: "Pelayanan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pelayanan_CaraKBTerakhirId",
                table: "Pelayanan",
                column: "CaraKBTerakhirId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pelayanan_AlatKontrasepsi_CaraKBTerakhirId",
                table: "Pelayanan",
                column: "CaraKBTerakhirId",
                principalTable: "AlatKontrasepsi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelayanan_AlatKontrasepsi_CaraKBTerakhirId",
                table: "Pelayanan");

            migrationBuilder.DropIndex(
                name: "IX_Pelayanan_CaraKBTerakhirId",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "AnakHidupLaki",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "AnakHidupPerempuan",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "CaraKBTerakhirId",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "StatusKB",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "UmurAnakTerkecilBulan",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "UmurAnakTerkecilTahun",
                table: "Pelayanan");
        }
    }
}
