using CloudStore.Application.Responses.Users;
using CloudStore.Domain.Abstractions.Core;
using MediatR;

namespace CloudStore.Application.UseCases.Users.Queries.GetAll;

public interface IGetAllUsersDataRequest : IDataRequest<Unit, List<UserResponse>>
{
}