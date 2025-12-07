using FluentValidation;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Domain.Validators;

public class AlterarClienteValidator : AbstractValidator<Cliente>
{
    public AlterarClienteValidator()
    {
        When(x => x.Ativo, () =>
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id do cliente é obrigatório.");

            When(x => !string.IsNullOrWhiteSpace(x.Nome), () =>
            {
                RuleFor(x => x.Nome)
                    .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres.")
                    .MaximumLength(255).WithMessage("Nome deve ter no máximo 255 caracteres.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage("Email deve ser válido.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Telefone), () =>
            {
                RuleFor(x => x.Telefone).Matches(@"^\(?\d{2}\)?\s?[2-8]\d{3}-?\d{4}$").WithMessage("Telefone inválido.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Celular), () =>
            {
                RuleFor(x => x.Celular).Matches(@"^\(?\d{2}\)?\s?9\d{4}-?\d{4}$").WithMessage("Celular inválido.");
            });
        });
    }
}