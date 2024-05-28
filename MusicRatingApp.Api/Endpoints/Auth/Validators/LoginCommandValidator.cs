using FluentValidation;
using MusicRatingApp.Api.Endpoints.Auth.Commands;

namespace MusicRatingApp.Api.Endpoints.Auth.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}