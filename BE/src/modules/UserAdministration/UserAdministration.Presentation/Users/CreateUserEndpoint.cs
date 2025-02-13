using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UserAdministration.Business.Users.Create;

namespace UserAdministration.Presentation.Users;

public class CreateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async (CreateUserRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<CreateUserCommand>();

                var response = await sender.Send(command, cancellationToken);

                return response is not null ? Results.Ok(response) : Results.BadRequest();
            })
            .WithName("CreateUserEndpoint")
            .WithDisplayName("Create User")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}