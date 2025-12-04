using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Infra.Data.Mappings;

public class ItemOrdemMap : EntityMapBase<ItemOrdem>
{
    public override void Configure(EntityTypeBuilder<ItemOrdem> entity)
    {
        base.Configure(entity);

        entity.ToTable("item_ordem");

        entity.Property(x => x.NomeProduto).HasColumnName("nome_produto")
            .HasMaxLength(255).IsRequired();
        entity.Property(x => x.Quantidade).HasColumnName("quantidade").IsRequired();
        entity.Property(x => x.ValorUnitario).HasColumnName("valor_unitario")
            .HasColumnType("numeric(18,2)").IsRequired();
        entity.Property(x => x.OrdemId).HasColumnName("ordem_id").IsRequired();

        entity.HasIndex(x => x.OrdemId).HasDatabaseName("item_ordem_ordem_id_idx");
        entity.HasIndex(x => x.NomeProduto).HasDatabaseName("item_ordem_nome_produto_idx");

        entity.Ignore(x => x.ValorTotal);
    }
}