using Microsoft.EntityFrameworkCore.Migrations;

namespace KioskoTIC.Migrations
{
    public partial class CuartaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Iamgen1",
                table: "Proyectos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IamgenProyecto",
                table: "Proyectos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen2",
                table: "Proyectos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen3",
                table: "Proyectos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URLVideo",
                table: "Proyectos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iamgen1",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "IamgenProyecto",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "Imagen2",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "Imagen3",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "URLVideo",
                table: "Proyectos");
        }
    }
}
