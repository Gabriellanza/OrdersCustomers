using FluentValidation;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Domain.Validators;

public class CriarClienteValidator : AbstractValidator<Cliente>
{
    public CriarClienteValidator()
    {
        RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("Nome é Obrigatório.")
            .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(255).WithMessage("Nome deve ter no máximo 255 caracteres.");

        RuleFor(x => x.CpfCnpj).NotEmpty().WithMessage("CPF/CNPJ é obrigatório.")
            .Must(GenericoValidator.CpfOuCnpjValido).WithMessage("CPF ou CNPJ inválido.");

        When(x => !string.IsNullOrEmpty(x.Email), () =>
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email deve ser válido.");
        });

        When(x => !string.IsNullOrEmpty(x.Telefone), () =>
        {
            RuleFor(x => x.Telefone).Matches(@"^\(?\d{2}\)?\s?[2-8]\d{3}-?\d{4}$").WithMessage("Telefone inválido.");
        });

        When(x => !string.IsNullOrEmpty(x.Celular), () =>
        {
            RuleFor(x => x.Celular).Matches(@"^\(?\d{2}\)?\s?9\d{4}-?\d{4}$").WithMessage("Celular inválido.");
        });
    }
}