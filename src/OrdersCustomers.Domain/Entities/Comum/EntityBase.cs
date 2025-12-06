using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using FluentValidation;
using OrdersCustomers.Domain.Interfaces.Comum;
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

    #endregion

}