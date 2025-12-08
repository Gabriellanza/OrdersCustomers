using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Infra.Data.Mappings;

public class ItemOrdemMap : EntityMapBase<ItemOrdem>
{
    public override void Configure(EntityTypeBuilder<ItemOrdem> builder)
    {
        base.Configure(builder);

        builder.ToTable("item_ordem");

        builder.Property(x => x.NomeProduto).HasColumnName("nome_produto")
            .HasMaxLength(255).IsRequired();
        builder.Property(x => x.Quantidade).HasColumnName("quantidade").IsRequired();
        builder.Property(x => x.ValorUnitario).HasColumnName("valor_unitario")
            .HasColumnType("numeric(18,2)").IsRequired();
        builder.Property(x => x.OrdemId).HasColumnName("ordem_id").IsRequired();

        builder.HasIndex(x => x.OrdemId).HasDatabaseName("item_ordem_ordem_id_idx");
        builder.HasIndex(x => x.NomeProduto).HasDatabaseName("item_ordem_nome_produto_idx");

        builder.Ignore(x => x.ValorTotal);
    }
}