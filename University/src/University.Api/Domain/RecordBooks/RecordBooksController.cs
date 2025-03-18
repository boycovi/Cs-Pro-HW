using MediatR;
using Microsoft.AspNetCore.Mvc;
using University.Api.Common;
using University.Api.Domain.RecordBooks.Request;
using University.Application.Domain.RecordBooks.Commands.CreateMark;
using University.Application.Domain.RecordBooks.Commands.CreateRecordBook;
using University.Application.Domain.RecordBooks.Commands.RemoveMark;
using University.Application.Domain.RecordBooks.Commands.RemoveRecordBook;
using University.Application.Domain.RecordBooks.Queries.GetRecordBook;

namespace University.Api.Domain.RecordBooks;

[ApiController]
[Route(Routs.RecordBooks)]
public class RecordBooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecordBooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<RecordBookDto[]> GetRecordBook(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetRecordBookQuery(pageNumber, pageSize);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<Guid> CreateRecordBook([FromBody] CreateRecordBookRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateRecordBookCommand(request.StudentId);
        var id = await _mediator.Send(command, cancellationToken);
        return id;
    }

    [HttpPost("add-marks")]
    public async Task CreateMarks([FromBody] CreateMarksRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMarkCommand(request.RecordBookId, request.SubjectId, request.Grade);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    public async Task DeleteRecordBook([FromBody] RemoveRecordBookRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveRecordBookCommand(request.Id);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete("delete-marks")]
    public async Task DeleteMarks([FromBody] RemoveMarksRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveMarkCommand(request.RecordId);
        await _mediator.Send(command, cancellationToken);
    }
}