using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleGastosResidenciais.Migrations
{
    /// <inheritdoc />
    public partial class UseFluentConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Transacao_Valor",
                table: "Transacoes",
                sql: "\"Valor\" >= 0.01");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Pessoa_Idade",
                table: "Pessoas",
                sql: "\"Idade\" >= 0 AND \"Idade\" <= 150");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Transacao_Valor",
                table: "Transacoes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Pessoa_Idade",
                table: "Pessoas");
        }
    }
}
