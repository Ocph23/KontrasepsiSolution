using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainWeb.Migrations
{
    public partial class pekerjaanText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PekerjaanPasanganText",
                table: "Peserta",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PekerjaanText",
                table: "Peserta",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PekerjaanPasanganText",
                table: "Peserta");

            migrationBuilder.DropColumn(
                name: "PekerjaanText",
                table: "Peserta");
        }
    }
}
