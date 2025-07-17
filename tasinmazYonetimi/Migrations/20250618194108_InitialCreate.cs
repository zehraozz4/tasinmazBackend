using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace tasinmazYonetimi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Il",
                columns: table => new
                {
                    ilId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ilAd = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Il", x => x.ilId);
                });

            migrationBuilder.CreateTable(
                name: "Kullanici",
                columns: table => new
                {
                    kullaniciId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    kullaniciAd = table.Column<string>(type: "text", nullable: false),
                    kullaniciSoyad = table.Column<string>(type: "text", nullable: false),
                    eMail = table.Column<string>(type: "text", nullable: false),
                    parola = table.Column<string>(type: "text", nullable: false),
                    rol = table.Column<string>(type: "text", nullable: false),
                    adres = table.Column<string>(type: "text", nullable: false),
                    eklenmeTarihi = table.Column<string>(type: "text", nullable: false),
                    guncellemeTarihi = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanici", x => x.kullaniciId);
                });

            migrationBuilder.CreateTable(
                name: "Ilce",
                columns: table => new
                {
                    ilceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ilceAd = table.Column<string>(type: "text", nullable: false),
                    ilId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilce", x => x.ilceId);
                    table.ForeignKey(
                        name: "FK_Ilce_Il_ilId",
                        column: x => x.ilId,
                        principalTable: "Il",
                        principalColumn: "ilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    logId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    kullaniciId = table.Column<int>(type: "integer", nullable: false),
                    durum = table.Column<string>(type: "text", nullable: false),
                    islemTipi = table.Column<string>(type: "text", nullable: false),
                    tarihSaat = table.Column<string>(type: "text", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    aciklama = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.logId);
                    table.ForeignKey(
                        name: "FK_Log_Kullanici_kullaniciId",
                        column: x => x.kullaniciId,
                        principalTable: "Kullanici",
                        principalColumn: "kullaniciId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mahalle",
                columns: table => new
                {
                    mahalleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mahalleAd = table.Column<string>(type: "text", nullable: false),
                    ilceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mahalle", x => x.mahalleId);
                    table.ForeignKey(
                        name: "FK_Mahalle_Ilce_ilceId",
                        column: x => x.ilceId,
                        principalTable: "Ilce",
                        principalColumn: "ilceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasinmaz",
                columns: table => new
                {
                    tasinmazId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ilId = table.Column<int>(type: "integer", nullable: false),
                    ilceId = table.Column<int>(type: "integer", nullable: false),
                    mahalleId = table.Column<int>(type: "integer", nullable: false),
                    adaa = table.Column<string>(type: "text", nullable: false),
                    parsel = table.Column<string>(type: "text", nullable: false),
                    nitelik = table.Column<string>(type: "text", nullable: false),
                    adres = table.Column<string>(type: "text", nullable: false),
                    eklenmeTarihi = table.Column<string>(type: "text", nullable: false),
                    kullaniciId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasinmaz", x => x.tasinmazId);
                    table.ForeignKey(
                        name: "FK_Tasinmaz_Il_ilId",
                        column: x => x.ilId,
                        principalTable: "Il",
                        principalColumn: "ilId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasinmaz_Ilce_ilceId",
                        column: x => x.ilceId,
                        principalTable: "Ilce",
                        principalColumn: "ilceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasinmaz_Kullanici_kullaniciId",
                        column: x => x.kullaniciId,
                        principalTable: "Kullanici",
                        principalColumn: "kullaniciId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasinmaz_Mahalle_mahalleId",
                        column: x => x.mahalleId,
                        principalTable: "Mahalle",
                        principalColumn: "mahalleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ilce_ilId",
                table: "Ilce",
                column: "ilId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_kullaniciId",
                table: "Log",
                column: "kullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Mahalle_ilceId",
                table: "Mahalle",
                column: "ilceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasinmaz_ilceId",
                table: "Tasinmaz",
                column: "ilceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasinmaz_ilId",
                table: "Tasinmaz",
                column: "ilId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasinmaz_kullaniciId",
                table: "Tasinmaz",
                column: "kullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasinmaz_mahalleId",
                table: "Tasinmaz",
                column: "mahalleId");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Tasinmaz");

            migrationBuilder.DropTable(
                name: "Kullanici");

            migrationBuilder.DropTable(
                name: "Mahalle");

            migrationBuilder.DropTable(
                name: "Ilce");

            migrationBuilder.DropTable(
                name: "Il");
        }
    }
}
