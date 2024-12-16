using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seguro.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Propostas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VeiculoMarca = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    VeiculoModelo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    VeiculoAno = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    VeiculoValor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Condutor_Cpf = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Condutor_DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CondutorEstado = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    CondutorLocalidade = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    CondutorBairro = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    CondutorLogradouro = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    CondutorNumero = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    CondutorComplemento = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    ProprietarioCpf = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    ProprietarioNome = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    ProprietarioDataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProprietarioEstado = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    ProprietarioLocalidade = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    ProprietarioBairro = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    ProprietarioLogradouro = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    ProprietarioNumero = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    ProprietarioComplemento = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HistoricoAcidentes = table.Column<int>(type: "int", nullable: false),
                    NivelRisco = table.Column<int>(type: "int", nullable: false),
                    PontuacaoNivelRisco = table.Column<int>(type: "int", nullable: false),
                    ValorTotalApolice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propostas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propostas");
        }
    }
}
