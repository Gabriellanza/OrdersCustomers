using OrdersCustomers.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace OrdersCustomers.Infra.Data.Mappings;

public class ClienteMap : EntityMapBase<Cliente>
{
    public override void Configure(EntityTypeBuilder<Cliente> entity)
    {
        base.Configure(entity);

        entity.ToTable("cliente");

        entity.Property(x => x.CpfCnpj).HasColumnName("cpf_cnpj").HasMaxLength(50).IsRequired(false);
        entity.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(255).IsRequired();
        entity.Property(x => x.Email).HasColumnName("email").HasMaxLength(255).IsRequired(false);
        entity.Property(x => x.Telefone).HasColumnName("telefone").HasMaxLength(20).IsRequired(false);
        entity.Property(x => x.Celular).HasColumnName("celular").HasMaxLength(20).IsRequired(false);

        entity.HasMany(x => x.EnderecoList).WithOne().HasForeignKey(x => x.ClienteId).IsRequired(false);

        entity.HasIndex(x => x.CpfCnpj).HasDatabaseName("cliente_cpf_cnpj_idx").IsUnique();
        entity.HasIndex(x => x.Nome).HasDatabaseName("cliente_nome_idx");
    }
}