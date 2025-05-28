using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelanceTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddSirketNavigationToProje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SirketId",
                table: "Projeler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SirketId",
                table: "Projeler");
        }
    }
}
