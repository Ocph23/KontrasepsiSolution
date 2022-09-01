using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainWeb.Migrations
{
    public partial class _tanggalKembali : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelayanan_AlatKontrasepsi_CaraKBTerakhirId",
                table: "Pelayanan");

            migrationBuilder.AlterColumn<int>(
                name: "CaraKBTerakhirId",
                table: "Pelayanan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "TanggalKembali",
                table: "Pelayanan",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pelayanan_AlatKontrasepsi_CaraKBTerakhirId",
                table: "Pelayanan",
                column: "CaraKBTerakhirId",
                principalTable: "AlatKontrasepsi",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelayanan_AlatKontrasepsi_CaraKBTerakhirId",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "TanggalKembali",
                table: "Pelayanan");

            migrationBuilder.AlterColumn<int>(
                name: "CaraKBTerakhirId",
                table: "Pelayanan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pelayanan_AlatKontrasepsi_CaraKBTerakhirId",
                table: "Pelayanan",
                column: "CaraKBTerakhirId",
                principalTable: "AlatKontrasepsi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
