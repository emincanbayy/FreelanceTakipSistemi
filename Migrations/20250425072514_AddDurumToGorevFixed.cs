using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelanceTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddDurumToGorevFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Yorum_Gorevler_GorevId",
                table: "Yorum");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Yorum",
                table: "Yorum");

            migrationBuilder.RenameTable(
                name: "Yorum",
                newName: "Yorumlar");

            migrationBuilder.RenameIndex(
                name: "IX_Yorum_GorevId",
                table: "Yorumlar",
                newName: "IX_Yorumlar_GorevId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Yorumlar",
                table: "Yorumlar",
                column: "YorumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Yorumlar_Gorevler_GorevId",
                table: "Yorumlar",
                column: "GorevId",
                principalTable: "Gorevler",
                principalColumn: "GorevId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Yorumlar_Gorevler_GorevId",
                table: "Yorumlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Yorumlar",
                table: "Yorumlar");

            migrationBuilder.RenameTable(
                name: "Yorumlar",
                newName: "Yorum");

            migrationBuilder.RenameIndex(
                name: "IX_Yorumlar_GorevId",
                table: "Yorum",
                newName: "IX_Yorum_GorevId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Yorum",
                table: "Yorum",
                column: "YorumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Yorum_Gorevler_GorevId",
                table: "Yorum",
                column: "GorevId",
                principalTable: "Gorevler",
                principalColumn: "GorevId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
