using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelanceTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddKullaniciIdToYorum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KullaniciId",
                table: "Yorumlar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_KullaniciId",
                table: "Yorumlar",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Yorumlar_Kullanicilar_KullaniciId",
                table: "Yorumlar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Yorumlar_Kullanicilar_KullaniciId",
                table: "Yorumlar");

            migrationBuilder.DropIndex(
                name: "IX_Yorumlar_KullaniciId",
                table: "Yorumlar");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Yorumlar");
        }
    }
}
