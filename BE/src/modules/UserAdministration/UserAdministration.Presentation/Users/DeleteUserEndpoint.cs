using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UserAdministration.Business.Users.Delete;

namespace UserAdministration.Presentation.Users;

public class DeleteUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("users/{id:guid}",
                async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new DeleteUserCommand(id);

                    var isSuccessful = await sender.Send(command, cancellationToken);

                    return isSuccessful ? Results.NoContent() : Results.NotFound();
                })
            .WithName("DeleteUserEndpoint")
            .WithDisplayName("Delete User")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}