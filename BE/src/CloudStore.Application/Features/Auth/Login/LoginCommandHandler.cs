using CloudStore.Application.Abstractions;
using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Domain.Exceptions.Users;
using MediatR;

namespace CloudStore.Application.Features.Auth.Login;

public sealed class LoginCommandHandler(
    IUserReadRepository userReadRepository,
    IPasswordHasher passwordHasher,
    ITokenGenerator tokenGenerator) : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.GetByEmailAsync(request.Email);

        if (user is null)
            throw new UserNotFoundException(request.Email);

        var isPasswordValid = passwordHasher.VerifyPassword(user.Id, request.Password, user.PasswordHash);

        if (!isPasswordValid)
            throw new UserPasswordInvalidException();

        if (!user.IsEmailVerified)
            throw new UserEmailNotVerifiedException();

        var token = tokenGenerator.Generate(user.Id, user.Email);

        return new LoginResponse(user.Id, user.Email, token);
    }
}