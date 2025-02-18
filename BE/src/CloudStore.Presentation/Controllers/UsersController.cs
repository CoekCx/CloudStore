using CloudStore.Application.Responses.Users;
using CloudStore.Application.UseCases.Users.Create;
using CloudStore.Application.UseCases.Users.Delete;
using CloudStore.Application.UseCases.Users.GetAll;
using CloudStore.Application.UseCases.Users.GetById;
using CloudStore.Application.UseCases.Users.Update;
using CloudStore.Presentation.DTOs.Requests.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CloudStore.Presentation.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UsersController(IMediator sender) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Retrieves a list of all users in the system")]
    [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Get user by ID",
        Description = "Retrieves a specific user by their unique identifier")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create new user",
        Description = "Creates a new user in the system")]
    [ProducesResponseType(typeof(UserCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName);

        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Update user",
        Description = "Updates an existing user's information")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUser(
        [FromRoute] Guid id,
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(id, request.FirstName, request.LastName);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Delete user",
        Description = "Deletes a user from the system")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }
}