using CloudStore.Application.DTOs.Responses.Files;
using CloudStore.Application.Features.Files.Create;
using CloudStore.Application.Features.Files.Delete;
using CloudStore.Application.Features.Files.GetById;
using CloudStore.Application.Features.Files.Move;
using CloudStore.Application.Features.Files.Update;
using CloudStore.Presentation.DTOs.Requests.Files;
using CloudStore.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CloudStore.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/files")]
[Produces("application/json")]
public class FilesController(IMediator sender) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create new file",
        Description = "Creates a new file in the system")]
    [ProducesResponseType(typeof(FileResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateFile(
        [FromBody] CreateFileRequest request,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var command = new CreateFileCommand(request.File, request.ParentDirectoryId);
        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetFile), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Get file by ID",
        Description = "Retrieves a specific file by its unique identifier")]
    [ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFile([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var query = new GetFileByIdQuery(id, userId);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Update file name",
        Description = "Updates the name of an existing file")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateFile(
        [FromRoute] Guid id,
        [FromBody] UpdateFileRequest request,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var command = new UpdateFileCommand(id, userId, request.Name);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id:guid}/move")]
    [SwaggerOperation(
        Summary = "Move file",
        Description = "Moves a file to a new parent directory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MoveFile(
        [FromRoute] Guid id,
        [FromBody] MoveFileRequest request,
        CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var command = new MoveFileCommand(id, userId, request.NewParentDirectoryId);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Delete file",
        Description = "Deletes a file")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteFile([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.GetUserIdentityId();

        var command = new DeleteFileCommand(id, userId);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }
}