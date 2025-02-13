using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UserAdministration.Business.Users.Update;

namespace UserAdministration.Presentation.Users;

public class UpdateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id:guid}",
                async (Guid id, UpdateUserRequest request, ISender sender, CancellationToken token) =>
                {
                    var command = new UpdateUserCommand(id, request.FirstName, request.LastName);

                    await sender.Send(command, token);

                    return Results.NoContent();
                })
            .WithName("UpdateUserEndpoint")
            .WithDisplayName("Update User")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}