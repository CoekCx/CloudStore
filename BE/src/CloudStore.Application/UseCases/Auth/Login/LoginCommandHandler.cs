using CloudStore.Application.Abstractions;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Auth.Login;

public sealed class LoginCommandHandler(
    IUserReadRepository userReadRepository,
    IPasswordHasher passwordHasher,
    ITokenGenerator tokenGenerator) : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(request.Email);
        }

        var isValid = passwordHasher.VerifyPassword(
            user.Id,
            request.Password,
            user.PasswordHash);

        if (!isValid)
        {
            throw new UserPasswordInvalidException();
        }

        if (!user.IsEmailVerified)
        {
            throw new UserEmailNotVerifiedException();
        }

        return tokenGenerator.Generate(user.Id, user.Email);
    }
}