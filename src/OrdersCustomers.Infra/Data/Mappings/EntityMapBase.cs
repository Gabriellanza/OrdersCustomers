using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersCustomers.Domain.Entities.Comum;

namespace OrdersCustomers.Infra.Data.Mappings;

public abstract class EntityMapBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.UsuarioAlteracao).HasColumnName("usuario_alteracao");
        builder.Property(e => e.DataAtualizacao).HasColumnName("data_atualizacao");
        builder.Property(e => e.UsuarioCriacao).HasColumnName("usuario_criacao").IsRequired();
        builder.Property(e => e.DataCriacao).HasColumnName("data_criacao").IsRequired();
        builder.Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
        
        builder.HasIndex(x => x.Ativo).HasDatabaseName($"{typeof(TEntity).Name.ToLower()}_ativo_idx");
        builder.HasIndex(x => x.DataCriacao).HasDatabaseName($"{typeof(TEntity).Name.ToLower()}_data_criacao_idx");
        builder.HasIndex(x => new { x.Ativo, x.DataCriacao }).IsDescending(false, true).HasDatabaseName($"{typeof(TEntity).Name.ToLower()}_ativo_data_criacao_desc_idx");

        builder.Ignore(x => x.ValidationResult);
    }
}
