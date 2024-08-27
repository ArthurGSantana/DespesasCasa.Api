using DespesasCasa.Domain.Model.Dto;
using FluentValidation;

namespace DespesasCasa.Domain.Validators;

public class CollectionDtoValidator : AbstractValidator<CollectionDto>
{
    public CollectionDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Invalid title.");
    }
}
