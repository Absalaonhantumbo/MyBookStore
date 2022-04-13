using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ChangecpfM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Clients",
                newName: "Cpf");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Clients",
                newName: "Cnpj");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Clients",
                newName: "CPF");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "Clients",
                newName: "CNPJ");
        }
    }
}
