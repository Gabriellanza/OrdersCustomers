using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersCustomers.Domain.Entities.Comum;

namespace OrdersCustomers.Infra.Data.Mappings;

public abstract class EntityMapBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> entity)
    {
        entity.HasKey(x => x.Id);
        entity.Property(e => e.UsuarioAlteracao).HasColumnName("usuario_alteracao");
        entity.Property(e => e.DataAtualizacao).HasColumnName("data_atualizacao");
        entity.Property(e => e.UsuarioCriacao).HasColumnName("usuario_criacao").IsRequired();
        entity.Property(e => e.DataCriacao).HasColumnName("data_criacao").IsRequired();
        entity.Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
        entity.HasQueryFilter(x => x.Ativo == true);

        entity.HasIndex(x => x.Ativo).HasDatabaseName($"{typeof(TEntity).Name.ToLower()}_ativo_idx");
        entity.HasIndex(x => x.DataCriacao).HasDatabaseName($"{typeof(TEntity).Name.ToLower()}_data_criacao_idx");
        entity.HasIndex(x => new { x.Ativo, x.DataCriacao }).IsDescending(false, true).HasDatabaseName($"{typeof(TEntity).Name.ToLower()}_ativo_data_criacao_desc_idx");

        entity.Ignore(x => x.ValidationResult);
    }
}
