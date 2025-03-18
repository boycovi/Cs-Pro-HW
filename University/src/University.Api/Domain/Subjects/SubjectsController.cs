using MediatR;
using Microsoft.AspNetCore.Mvc;
using University.Api.Common;
using University.Api.Domain.Subjects.Request;
using University.Application.Domain.Subjects.Commands.CreateSubject;
using University.Application.Domain.Subjects.Commands.RemoveSubject;
using University.Application.Domain.Subjects.Commands.UpdateSubject;
using University.Application.Domain.Subjects.Queries.GetSubjects;

namespace University.Api.Domain.Subjects;

[ApiController]
[Route(Routs.Subjects)]
public class SubjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<SubjectDto[]> GetSubjects(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetSubjectsQuery(pageNumber, pageSize);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<Guid> CreateSubject([FromBody]CreateSubjectRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSubjectCommand(request.Name, request.Code);
        var id = await _mediator.Send(command, cancellationToken);
        return id;
    }

    [HttpPut]
    public async Task UpdateSubject([FromBody] UpdateSubjectRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSubjectCommand(request.Id, request.Name, request.Code);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    public async Task DeleteSubject([FromBody] RemoveSubjectRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveSubjectCommand(request.Id);
        await _mediator.Send(command, cancellationToken);
    }
}