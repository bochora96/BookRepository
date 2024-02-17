using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemoDemo.Application.Features.Book.Commands.Add;
using TemoDemo.Application.Features.Book.Commands.Delete;
using TemoDemo.Application.Features.Book.Commands.Update;
using TemoDemo.Application.Features.Book.Queries.GetAll;
using TemoDemo.Application.Features.Book.Queries.GetById;
using TemoDemo.Application.Responses;

namespace DemoTemo.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BookController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<GetAllDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllQuery(), cancellationToken);

        return Ok(new BaseResponse<GetAllDto> { Success = true, Response = result });
    }

    [HttpGet("{id:guid}", Name = "GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<GetByIdDto>>> GetById(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var result = await sender.Send(new GetByIdQuery(id), cancellationToken);

        return Ok(new BaseResponse<GetByIdDto> { Success = true, Response = result });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<AddDto>>> Add(
        AddCommand addCommand,
        CancellationToken cancellationToken
    )
    {
        var result = await sender.Send(addCommand, cancellationToken);

        return CreatedAtRoute(
            nameof(GetById),
            new { id = result.Id },
            new BaseResponse<AddDto> { Success = true, Response = result }
        );
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<UpdateDto>>> Add(
        Guid id,
        UpdateRequest updateRequest,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateCommand(
            id,
            updateRequest.Title,
            updateRequest.Author,
            updateRequest.DateOfPublication
        );

        _ = await sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<DeleteDto>>> Delete(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        _ = await sender.Send(new DeleteCommand(id), cancellationToken);

        return NoContent();
    }
}
