using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UserAdministration.Business.Users.GetAll;
using UserAdministration.Business.Users.GetById;

namespace UserAdministration.Presentation.Users;

public class GetUserByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetUserByIdQuery(id);

                var user = await sender.Send(query, cancellationToken);

                return Results.Ok(user);
            })
            .WithName("GetUserByIdEndpoint")
            .WithDisplayName("Get User by ID")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}