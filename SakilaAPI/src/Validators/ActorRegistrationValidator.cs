using FluentValidation;
using SakilaAPI.DTOs.Actor;

namespace SakilaAPI.Validators;

public class ActorRegistrationValidator : AbstractValidator<ActorRegistrationDto>
{
    public ActorRegistrationValidator()
    {
        RuleFor(x => x.FirstName)
        .NotEmpty()
        .Length(2, 16)
        .NotEqual("string", StringComparer.OrdinalIgnoreCase)
        .Must(name => name.All(c =>
        char.IsLetter(c)
        || c == ' '
        || c == '-'
        )).WithMessage("{PropertyName} may only contain letters, spaces or hyphens.");       

        RuleFor(x => x.LastName)
        .NotEmpty()
        .Length(2, 16)
        .NotEqual("string", StringComparer.OrdinalIgnoreCase)
        .Must(name => name.All(c =>
        char.IsLetter(c)
        || c == ' '
        || c == '-'
        )).WithMessage("{PropertyName} may only contain letters, spaces or hyphens.");       
    }
}