using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainWeb.Migrations
{
    public partial class pemeriksaan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TekananDarah",
                table: "Pelayanan",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Diabetes",
                table: "Pelayanan",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "KeganasanGinekologi",
                table: "Pelayanan",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "KeganasanGinekologi2",
                table: "Pelayanan",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PemekuanDarah",
                table: "Pelayanan",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Radang",
                table: "Pelayanan",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RadangOrchitis",
                table: "Pelayanan",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diabetes",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "KeganasanGinekologi",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "KeganasanGinekologi2",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "PemekuanDarah",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "Radang",
                table: "Pelayanan");

            migrationBuilder.DropColumn(
                name: "RadangOrchitis",
                table: "Pelayanan");

            migrationBuilder.AlterColumn<double>(
                name: "TekananDarah",
                table: "Pelayanan",
                type: "double",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
