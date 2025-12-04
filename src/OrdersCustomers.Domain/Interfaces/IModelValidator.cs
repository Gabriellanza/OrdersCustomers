using FluentValidation;
using FluentValidation.Results;

namespace OrdersCustomers.Domain.Interfaces;

public interface IModelValidator
{
    bool EhValido();

    bool EhValidoAlterar();

    bool EhValidoRemover();

    ValidationResult ValidationResult { get; set; }

    bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator);
}