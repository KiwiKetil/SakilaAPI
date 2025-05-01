using FluentValidation;
using SakilaAPI.DTOs.Actor;

namespace SakilaAPI.Validators;

public class ActorUpdateValidator : AbstractValidator<ActorUpdateDto>
{
    public ActorUpdateValidator()
    {
        RuleFor(x => x.FirstName)
        .NotEmpty()
        .MaximumLength(16)
        .NotEqual("string", StringComparer.OrdinalIgnoreCase);

        RuleFor(x => x.LastName)
        .NotEmpty()
        .MaximumLength(16)
        .NotEqual("string", StringComparer.OrdinalIgnoreCase);
    }
}

