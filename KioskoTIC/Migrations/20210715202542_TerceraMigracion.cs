using Microsoft.EntityFrameworkCore.Migrations;

namespace KioskoTIC.Migrations
{
    public partial class TerceraMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "archivopdf",
                table: "Carreras",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "archivopdf",
                table: "Carreras");
        }
    }
}
