using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace OrdersCustomers.Domain.Entities.Comum;

public abstract class EntityBase : IModelValidator
{
    public Guid Id { get; protected set; }

    public bool Ativo { get; protected set; }

    public string UsuarioCriacao { get; protected set; }

    public string UsuarioAlteracao { get; protected set; }

    public DateTime DataCriacao { get; protected set; }

    public DateTime? DataAtualizacao { get; protected set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.UtcNow;
        Ativo = true;
    }

    protected EntityBase(Guid id)
    {
        Id = id;
        DataCriacao = DateTime.UtcNow;
        Ativo = true;
    }

    #region Validation

    public virtual bool EhValido() => true;

    public virtual bool EhValidoAlterar() => EhValido();

    public virtual bool EhValidoRemover() => EhValido();

    [NotMapped]
    [IgnoreDataMember]
    public ValidationResult ValidationResult { get; set; }

    public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);
        return ValidationResult.IsValid;
    }

    public void Validate(IModel model, IDiagnosticsLogger<DbLoggerCategory.Model.Validation> logger)
    {
        throw new NotImplementedException();
    }

    #endregion


}