using FluentValidation;
using FluentValidation.Results;

namespace OrdersCustomers.Application.Interfaces.Comum;

public interface IModelValidator
{
    bool EhValido();

    bool EhValidoAlterar();

    bool EhValidoRemover();

    ValidationResult ValidationResult { get; set; }

    bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator);
}