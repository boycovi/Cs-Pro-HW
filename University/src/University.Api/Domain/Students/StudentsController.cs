using MediatR;
using Microsoft.AspNetCore.Mvc;
using University.Api.Common;
using University.Api.Domain.Students.Request;
using University.Application.Domain.Students.Commands.CreateStudent;
using University.Application.Domain.Students.Commands.RemoveStudent;
using University.Application.Domain.Students.Commands.UpdateStudent;
using University.Application.Domain.Students.Queries.GetStudent;

namespace University.Api.Domain.Students;

[ApiController]
[Route(Routs.Students)]
public class StudentsController :ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<StudentDto[]> GetStudents(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetStudentsQuery(pageNumber, pageSize);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<Guid> CreateStudent([FromBody] CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateStudentCommand(request.FirstName, request.LastName, request.MiddleName, request.GroupId);
        var id = await _mediator.Send(command, cancellationToken);
        return id;
    }

    [HttpPut]
    public async Task PutStudent([FromBody] UpdateStudentRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateStudentCommand(request.StudentId, request.FirstName, request.LastName, request.MiddleName);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    public async Task DeleteStudent([FromBody] RemoveStudentRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveStudentCommand(request.Id);
        await _mediator.Send(command, cancellationToken);
    }
}