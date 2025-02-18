namespace CloudStore.Application.Abstractions;

public interface IFileFormatter
{
    Uri Format(Guid fileId);
}