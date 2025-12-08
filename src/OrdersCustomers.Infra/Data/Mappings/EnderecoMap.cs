using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Infra.Data.Mappings;

public class EnderecoMap : EntityMapBase<Endereco>
{
    public override void Configure(EntityTypeBuilder<Endereco> builder)
    {
        base.Configure(builder);

        builder.ToTable("endereco");

        builder.Property(x => x.Logradouro).HasColumnName("logradouro").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Cep).HasColumnName("cep").HasMaxLength(12).IsRequired(false);
        builder.Property(x => x.Numero).HasColumnName("numero").HasMaxLength(20).IsRequired(false);
        builder.Property(x => x.Bairro).HasColumnName("bairro").HasMaxLength(255).IsRequired(false);
        builder.Property(x => x.Cidade).HasColumnName("cidade").HasMaxLength(255).IsRequired(false);
        builder.Property(x => x.Estado).HasColumnName("estado").HasMaxLength(2).IsRequired(false);

        builder.Property(x => x.ClienteId).HasColumnName("cliente_id").IsRequired();

        builder.HasIndex(x => x.Cep).HasDatabaseName("endereco_cep_idx");
        builder.HasIndex(x => x.ClienteId).HasDatabaseName("endereco_cliente_id_idx");
    }
}