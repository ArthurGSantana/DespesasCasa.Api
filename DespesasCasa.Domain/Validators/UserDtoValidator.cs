using DespesasCasa.Domain.Model.Dto;
using FluentValidation;

namespace DespesasCasa.Domain.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.Email)
            .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage($"Invalid e-mail.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Invalid password.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Invalid name.");
    }
}
