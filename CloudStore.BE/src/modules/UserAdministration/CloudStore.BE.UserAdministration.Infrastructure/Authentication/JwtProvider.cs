using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CloudStore.BE.UserAdministration.Business.Abstractions;
using Common.Contracts.Authentication;
using Common.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CloudStore.BE.UserAdministration.Infrastructure.Authentication;

public class JwtProvider(IOptions<JwtOptions> jwtOptions) : ITokenGenerator
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public string Generate(GenerateTokenRequest user)
    {
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}