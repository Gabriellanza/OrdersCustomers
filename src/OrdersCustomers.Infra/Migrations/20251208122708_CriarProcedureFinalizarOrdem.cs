using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersCustomers.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CriarProcedureFinalizarOrdem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "endereco_cliente_id_idx",
                table: "endereco");

            migrationBuilder.CreateIndex(
                name: "endereco_cliente_id_idx",
                table: "endereco",
                column: "cliente_id",
                unique: true);


            migrationBuilder.Sql(@"
                    CREATE OR REPLACE PROCEDURE FINALIZAR_ORDEM(ord_guid UUID)
                    LANGUAGE plpgsql
                    AS $$
                    BEGIN
                        UPDATE ordem
                        SET ""Status"" = 3
                        WHERE ""Id"" = ord_guid;
                    END;
                    $$;
                ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "endereco_cliente_id_idx",
                table: "endereco");

            migrationBuilder.CreateIndex(
                name: "endereco_cliente_id_idx",
                table: "endereco",
                column: "cliente_id");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS FINALIZAR_ORDEM(UUID);");
        }
    }
}
