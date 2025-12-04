using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Infra.Data.Mappings;

public class OrdemMap : EntityMapBase<Ordem>
{
    public override void Configure(EntityTypeBuilder<Ordem> entity)
    {
        base.Configure(entity);

        entity.ToTable("ordem");

        entity.Property(x => x.NumeroOrdem).HasColumnName("numero_ordem").IsRequired();
        entity.Property(x => x.DataConclusao).HasColumnName("data_conclusao");
        entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();

        entity.Property(x => x.ValorTotal).HasColumnName("valor_total")
            .HasColumnType("decimal(18,2)").IsRequired();

        entity.Property(x => x.ClienteId).HasColumnName("cliente_id").IsRequired();
        entity.HasOne(x => x.Cliente).WithMany().HasForeignKey(x => x.ClienteId);

        entity.HasMany(x => x.Itens).WithOne().HasForeignKey(x => x.OrdemId);

        entity.HasIndex(x => x.NumeroOrdem).HasDatabaseName("ordem_numero_ordem_idx").IsUnique();
        entity.HasIndex(x => x.ClienteId).HasDatabaseName("ordem_cliente_id_idx");
        entity.HasIndex(x => x.Status).HasDatabaseName("ordem_status_idx");
    }
}