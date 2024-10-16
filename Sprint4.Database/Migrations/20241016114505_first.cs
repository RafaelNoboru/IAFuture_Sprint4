using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sprint4.Database.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "iafuture_csharp_clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Sexo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Profissao = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    EstadoSaude = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iafuture_csharp_clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "iafuture_csharp_login",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Username = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iafuture_csharp_login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "iafuture_csharp_planos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Preco = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    TipoPlano = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Cobertura = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iafuture_csharp_planos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "iafuture_csharp_recomendacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClienteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PlanoSaudeId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataRecomendacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iafuture_csharp_recomendacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_iafuture_csharp_recomendacoes_iafuture_csharp_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "iafuture_csharp_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_iafuture_csharp_recomendacoes_iafuture_csharp_planos_PlanoSaudeId",
                        column: x => x.PlanoSaudeId,
                        principalTable: "iafuture_csharp_planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_iafuture_csharp_recomendacoes_ClienteId",
                table: "iafuture_csharp_recomendacoes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_iafuture_csharp_recomendacoes_PlanoSaudeId",
                table: "iafuture_csharp_recomendacoes",
                column: "PlanoSaudeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "iafuture_csharp_login");

            migrationBuilder.DropTable(
                name: "iafuture_csharp_recomendacoes");

            migrationBuilder.DropTable(
                name: "iafuture_csharp_clientes");

            migrationBuilder.DropTable(
                name: "iafuture_csharp_planos");
        }
    }
}
