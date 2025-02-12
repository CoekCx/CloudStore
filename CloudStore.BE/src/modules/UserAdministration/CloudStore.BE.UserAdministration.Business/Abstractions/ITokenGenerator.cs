using Common.Contracts.Authentication;

namespace CloudStore.BE.UserAdministration.Business.Abstractions;

public interface ITokenGenerator
{
    string Generate(GenerateTokenRequest user);
}