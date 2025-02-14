using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UserAdministration.Business.Users.GetAll;

namespace UserAdministration.Presentation.Users;

public class GetAllUsersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetAllUsersQuery();

                var users = await sender.Send(query, cancellationToken);

                return Results.Ok(users);
            })
            .WithName("GetAllUsersEndpoint")
            .WithDisplayName("Get All Users")
            .Produces(StatusCodes.Status200OK);
    }
}