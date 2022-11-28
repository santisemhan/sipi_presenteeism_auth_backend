using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPI_PRESENTEEISM.Data.Migrations
{
    public partial class adressactivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Activity");
        }
    }
}
