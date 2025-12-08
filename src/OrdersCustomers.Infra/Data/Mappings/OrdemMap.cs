using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Infra.Data.Mappings;

public class OrdemMap : EntityMapBase<Ordem>
{
    public override void Configure(EntityTypeBuilder<Ordem> builder)
    {
        base.Configure(builder);

        builder.ToTable("ordem");

        builder.Property(x => x.NumeroOrdem).HasColumnName("numero_ordem").IsRequired();
        builder.Property(x => x.DataConclusao).HasColumnName("data_conclusao");
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();

        builder.Property(x => x.ValorTotal).HasColumnName("valor_total")
            .HasColumnType("decimal(18,2)").IsRequired();

        builder.Property(x => x.ClienteId).HasColumnName("cliente_id").IsRequired();
        builder.HasOne(x => x.Cliente).WithMany().HasForeignKey(x => x.ClienteId);

        builder.HasMany(x => x.Itens).WithOne().HasForeignKey(x => x.OrdemId);

        builder.HasIndex(x => x.NumeroOrdem).HasDatabaseName("ordem_numero_ordem_idx").IsUnique();
        builder.HasIndex(x => x.ClienteId).HasDatabaseName("ordem_cliente_id_idx");
        builder.HasIndex(x => x.Status).HasDatabaseName("ordem_status_idx");
    }
}