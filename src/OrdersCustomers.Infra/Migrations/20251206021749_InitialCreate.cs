using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersCustomers.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    cpf_cnpj = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    celular = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    usuario_criacao = table.Column<string>(type: "text", nullable: false),
                    usuario_alteracao = table.Column<string>(type: "text", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    cep = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    logradouro = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    bairro = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cidade = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    cliente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    usuario_criacao = table.Column<string>(type: "text", nullable: false),
                    usuario_alteracao = table.Column<string>(type: "text", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_endereco_cliente_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "cliente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ordem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    numero_ordem = table.Column<long>(type: "bigint", nullable: false),
                    data_conclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    valor_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    cliente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    usuario_criacao = table.Column<string>(type: "text", nullable: false),
                    usuario_alteracao = table.Column<string>(type: "text", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ordem_cliente_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_ordem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome_produto = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    valor_unitario = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ordem_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    usuario_criacao = table.Column<string>(type: "text", nullable: false),
                    usuario_alteracao = table.Column<string>(type: "text", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_ordem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_item_ordem_ordem_ordem_id",
                        column: x => x.ordem_id,
                        principalTable: "ordem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "cliente_ativo_data_criacao_desc_idx",
                table: "cliente",
                columns: new[] { "ativo", "data_criacao" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "cliente_ativo_idx",
                table: "cliente",
                column: "ativo");

            migrationBuilder.CreateIndex(
                name: "cliente_cpf_cnpj_idx",
                table: "cliente",
                column: "cpf_cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "cliente_data_criacao_idx",
                table: "cliente",
                column: "data_criacao");

            migrationBuilder.CreateIndex(
                name: "cliente_nome_idx",
                table: "cliente",
                column: "nome");

            migrationBuilder.CreateIndex(
                name: "endereco_ativo_data_criacao_desc_idx",
                table: "endereco",
                columns: new[] { "ativo", "data_criacao" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "endereco_ativo_idx",
                table: "endereco",
                column: "ativo");

            migrationBuilder.CreateIndex(
                name: "endereco_cep_idx",
                table: "endereco",
                column: "cep");

            migrationBuilder.CreateIndex(
                name: "endereco_cliente_id_idx",
                table: "endereco",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "endereco_data_criacao_idx",
                table: "endereco",
                column: "data_criacao");

            migrationBuilder.CreateIndex(
                name: "item_ordem_nome_produto_idx",
                table: "item_ordem",
                column: "nome_produto");

            migrationBuilder.CreateIndex(
                name: "item_ordem_ordem_id_idx",
                table: "item_ordem",
                column: "ordem_id");

            migrationBuilder.CreateIndex(
                name: "itemordem_ativo_data_criacao_desc_idx",
                table: "item_ordem",
                columns: new[] { "ativo", "data_criacao" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "itemordem_ativo_idx",
                table: "item_ordem",
                column: "ativo");

            migrationBuilder.CreateIndex(
                name: "itemordem_data_criacao_idx",
                table: "item_ordem",
                column: "data_criacao");

            migrationBuilder.CreateIndex(
                name: "ordem_ativo_data_criacao_desc_idx",
                table: "ordem",
                columns: new[] { "ativo", "data_criacao" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "ordem_ativo_idx",
                table: "ordem",
                column: "ativo");

            migrationBuilder.CreateIndex(
                name: "ordem_cliente_id_idx",
                table: "ordem",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "ordem_data_criacao_idx",
                table: "ordem",
                column: "data_criacao");

            migrationBuilder.CreateIndex(
                name: "ordem_numero_ordem_idx",
                table: "ordem",
                column: "numero_ordem",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ordem_status_idx",
                table: "ordem",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "endereco");

            migrationBuilder.DropTable(
                name: "item_ordem");

            migrationBuilder.DropTable(
                name: "ordem");

            migrationBuilder.DropTable(
                name: "cliente");
        }
    }
}
