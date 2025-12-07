using FluentValidation;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Domain.Validators.Clientes;

public class CriarEnderecoValidator : AbstractValidator<Endereco>
{
    public CriarEnderecoValidator()
    {
        RuleFor(x => x.Cep)
            .NotEmpty().WithMessage("O CEP é obrigatório.")
            .Matches(@"^\d{8}$").WithMessage("O CEP deve conter 8 números.");

        RuleFor(x => x.Logradouro)
            .NotEmpty().WithMessage("O logradouro é obrigatório.")
            .MaximumLength(255).WithMessage("O logradouro pode ter no máximo 255 caracteres.");

        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("O número é obrigatório.")
            .MaximumLength(20).WithMessage("O número pode ter no máximo 20 caracteres.");

        RuleFor(x => x.Bairro)
            .NotEmpty().WithMessage("O bairro é obrigatório.")
            .MaximumLength(255).WithMessage("O bairro pode ter no máximo 255 caracteres.");

        RuleFor(x => x.Cidade)
            .NotEmpty().WithMessage("A cidade é obrigatória.")
            .MaximumLength(255).WithMessage("A cidade pode ter no máximo 255 caracteres.");

        RuleFor(x => x.Estado)
            .NotEmpty().WithMessage("O estado é obrigatório.")
            .Length(2).WithMessage("O estado deve conter exatamente 2 caracteres (UF).")
            .Matches(@"^[A-Za-z]{2}$").WithMessage("O estado deve conter apenas letras (UF).");
    }
}