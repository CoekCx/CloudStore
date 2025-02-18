using CloudStore.Application.DTOs.Responses.Directories;
using CloudStore.Application.Features.Directories.Create;
using CloudStore.Application.Features.Directories.Delete;
using CloudStore.Application.Features.Directories.GetById;
using CloudStore.Application.Features.Directories.GetContents;
using CloudStore.Application.Features.Directories.GetRoot;
using CloudStore.Application.Features.Directories.Move;
using CloudStore.Application.Features.Directories.Update;
using CloudStore.Presentation.DTOs.Requests.Directories;
using CloudStore.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CloudStore.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/directories")]
[Produces("application/json")]
public class DirectoriesController(IMediator sender) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create new directory",
        Description = "Creates a new directory in the system")]
    [ProducesResponseType(typeof(DirectoryResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDirectory(
        [FromBody] CreateDirectoryRequest request,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var command = new CreateDirectoryCommand(request.Name, userId, request.ParentDirectoryId);
        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetDirectory), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Get directory by ID",
        Description = "Retrieves a specific directory by its unique identifier")]
    [ProducesResponseType(typeof(DirectoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDirectory([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var query = new GetDirectoryByIdQuery(id, userId);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("root")]
    [SwaggerOperation(
        Summary = "Get user's root directory",
        Description = "Retrieves the root directory for the authenticated user")]
    [ProducesResponseType(typeof(DirectoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRootDirectory(CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var query = new GetRootDirectoryQuery(userId);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Update directory name",
        Description = "Updates the name of an existing directory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDirectory(
        [FromRoute] Guid id,
        [FromBody] UpdateDirectoryRequest request,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var command = new UpdateDirectoryCommand(id, userId, request.Name);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id:guid}/move")]
    [SwaggerOperation(
        Summary = "Move directory",
        Description = "Moves a directory to a new parent directory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MoveDirectory(
        [FromRoute] Guid id,
        [FromBody] MoveDirectoryRequest request,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var command = new MoveDirectoryCommand(id, userId, request.NewParentDirectoryId);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Delete directory",
        Description = "Deletes a directory and all its contents")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDirectory([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var command = new DeleteDirectoryCommand(id, userId);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}/contents")]
    [SwaggerOperation(
        Summary = "Get directory contents",
        Description = "Retrieves all files and subdirectories within a directory")]
    [ProducesResponseType(typeof(DirectoryContentsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDirectoryContents([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var query = new GetDirectoryContentsQuery(id, userId);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }
}