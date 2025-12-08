using OrdersCustomers.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace OrdersCustomers.Infra.Data.Mappings;

public class ClienteMap : EntityMapBase<Cliente>
{
    public override void Configure(EntityTypeBuilder<Cliente> builder)
    { 
        base.Configure(builder);

        builder.ToTable("cliente");

        builder.Property(x => x.CpfCnpj).HasColumnName("cpf_cnpj").HasMaxLength(50).IsRequired(false);
        builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(255).IsRequired(false);
        builder.Property(x => x.Telefone).HasColumnName("telefone").HasMaxLength(20).IsRequired(false);
        builder.Property(x => x.Celular).HasColumnName("celular").HasMaxLength(20).IsRequired(false);

        builder.HasOne(x => x.Endereco).WithOne(x => x.Cliente).HasForeignKey<Endereco>(x => x.ClienteId).IsRequired(false);

        builder.HasIndex(x => x.CpfCnpj).HasDatabaseName("cliente_cpf_cnpj_idx").IsUnique();
        builder.HasIndex(x => x.Nome).HasDatabaseName("cliente_nome_idx");
    }
}