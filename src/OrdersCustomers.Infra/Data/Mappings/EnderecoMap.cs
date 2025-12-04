using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Infra.Data.Mappings;

public class EnderecoMap : EntityMapBase<Endereco>
{
    public override void Configure(EntityTypeBuilder<Endereco> entity)
    {
        base.Configure(entity);

        entity.ToTable("endereco");

        entity.Property(x => x.Logradouro).HasColumnName("logradouro").HasMaxLength(255).IsRequired();
        entity.Property(x => x.Cep).HasColumnName("cep").HasMaxLength(12).IsRequired(false);
        entity.Property(x => x.Numero).HasColumnName("numero").HasMaxLength(20).IsRequired(false);
        entity.Property(x => x.Bairro).HasColumnName("bairro").HasMaxLength(255).IsRequired(false);
        entity.Property(x => x.Cidade).HasColumnName("cidade").HasMaxLength(255).IsRequired(false);
        entity.Property(x => x.Estado).HasColumnName("estado").HasMaxLength(2).IsRequired(false);

        entity.Property(x => x.ClienteId).HasColumnName("cliente_id").IsRequired();

        entity.HasIndex(x => x.Cep).HasDatabaseName("endereco_cep_idx");
        entity.HasIndex(x => x.ClienteId).HasDatabaseName("endereco_cliente_id_idx");
    }
}