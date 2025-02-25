using CloudStore.Application.Responses.Users;
using CloudStore.Domain.Abstractions.Core;
using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Application.UseCases.Users.Queries.GetById;

public interface IGetUserByIdDataRequest : IDataRequest<UserId, UserResponse>
{
}