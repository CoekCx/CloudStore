namespace CloudStore.Application.Abstractions;

public interface ITokenGenerator
{
    string Generate(Guid id, string email);
}