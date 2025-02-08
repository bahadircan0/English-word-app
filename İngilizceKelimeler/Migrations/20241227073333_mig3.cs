using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace İngilizceKelimeler.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewColumn",
                table: "English_Kelimeler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewColumn",
                table: "English_Kelimeler",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
