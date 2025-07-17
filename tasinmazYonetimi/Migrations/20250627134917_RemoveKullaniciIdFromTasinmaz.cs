using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tasinmazYonetimi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveKullaniciIdFromTasinmaz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasinmaz_Kullanici_kullaniciId",
                table: "Tasinmaz");

            migrationBuilder.DropIndex(
                name: "IX_Tasinmaz_kullaniciId",
                table: "Tasinmaz");

            migrationBuilder.DropColumn(
                name: "eklenmeTarihi",
                table: "Tasinmaz");

            migrationBuilder.DropColumn(
                name: "kullaniciId",
                table: "Tasinmaz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "tarihSaat",
                table: "Log",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "guncellemeTarihi",
                table: "Kullanici",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "eklenmeTarihi",
                table: "Kullanici",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "eklenmeTarihi",
                table: "Tasinmaz",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "kullaniciId",
                table: "Tasinmaz",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "tarihSaat",
                table: "Log",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "guncellemeTarihi",
                table: "Kullanici",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "eklenmeTarihi",
                table: "Kullanici",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_Tasinmaz_kullaniciId",
                table: "Tasinmaz",
                column: "kullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasinmaz_Kullanici_kullaniciId",
                table: "Tasinmaz",
                column: "kullaniciId",
                principalTable: "Kullanici",
                principalColumn: "kullaniciId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
