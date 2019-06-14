using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarefas.Data.Migrations
{
    public partial class TarefasMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EstaCompleta = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    DataConclusao = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefas");
        }
    }
}
