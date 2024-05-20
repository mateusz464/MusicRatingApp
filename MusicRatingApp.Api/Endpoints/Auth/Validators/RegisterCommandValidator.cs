using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MusicRatingApp.Api.Data;
using MusicRatingApp.Api.Endpoints.Auth.Commands;

namespace MusicRatingApp.Api.Endpoints.Auth.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly AppDbContext _dbContext;

    public RegisterCommandValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MustAsync(BeUniqueEmail).WithMessage("A user with this email already exists");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AllAsync(u => u.Email != email, cancellationToken);
    }
}