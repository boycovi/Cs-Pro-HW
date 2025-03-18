using MediatR;
using Microsoft.AspNetCore.Mvc;
using University.Api.Common;
using University.Api.Domain.Teachers.Request;
using University.Application.Domain.Teachers.Commands.CreateTeacher;
using University.Application.Domain.Teachers.Commands.CreateTeacherSubject;
using University.Application.Domain.Teachers.Commands.RemoveTeacher;
using University.Application.Domain.Teachers.Commands.RemoveTeacherSubject;
using University.Application.Domain.Teachers.Commands.UpdateTeacher;
using University.Application.Domain.Teachers.Queries.GetTeacher;

namespace University.Api.Domain.Teachers;

[ApiController]
[Route(Routs.Teachers)]
public class TeachersController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeachersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<TeacherDto[]> GetTeacher(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetTeachersQuery(pageNumber, pageSize);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<Guid> CreateTeacher([FromBody] CreateTeacherRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTeacherCommand(request.FirstName, request.LastName, request.MiddleName);
        var id = await _mediator.Send(command, cancellationToken);
        return id;
    }

    [HttpPost("add-subjects")]
    public async Task CreateSubjects([FromBody] CreateTeacherSubjectsRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTeacherSubjectCommand(request.TeacherId, request.SubjectId);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPut]
    public async Task UpdateTeacher([FromBody] UpdateTeacherRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateTeacherCommand(request.TeacherId, request.FirstName, request.LastName, request.MiddleName);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    public async Task DeleteTeacher([FromBody] RemoveTeacherRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveTeacherCommand(request.Id);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete("delete-subjects")]
    public async Task DeleteSubjects([FromBody] RemoveTeacherSubjectRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveTeacherSubjectCommand(request.TeacherId);
        await _mediator.Send(command, cancellationToken);
    }
}