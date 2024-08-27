using DespesasCasa.Domain.Model.Dto;
using FluentValidation;

namespace DespesasCasa.Domain.Validators;

public class ExpenseDtoValidator : AbstractValidator<ExpenseDto>
{
    public ExpenseDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Invalid title.");

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Invalid value.");

        RuleFor(x => x.PaymentDate)
            .NotEmpty()
            .WithMessage("Invalid date.");
    }
}
