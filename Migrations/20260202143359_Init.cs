using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicos.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Especialidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medico_Especialidad_Especialidad",
                        column: x => x.Especialidad,
                        principalTable: "Especialidad",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medico_Especialidad",
                table: "Medico",
                column: "Especialidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Especialidad");
        }
    }
}
