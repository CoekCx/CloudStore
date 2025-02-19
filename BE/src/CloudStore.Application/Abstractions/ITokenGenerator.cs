using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Application.Abstractions;

public interface ITokenGenerator
{
    string Generate(UserId id, string email);
}