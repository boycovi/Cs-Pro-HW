using MediatR;
using Microsoft.AspNetCore.Mvc;
using University.Api.Common;
using University.Api.Domain.Faculties.Request;
using University.Application.Domain.Faculties.Commands.CreateFaculty;
using University.Application.Domain.Faculties.Commands.CreateFacultyDepartments;
using University.Application.Domain.Faculties.Commands.RemoveFaculty;
using University.Application.Domain.Faculties.Commands.RemoveFacultyDepartments;
using University.Application.Domain.Faculties.Queries.GetFaculty;

namespace University.Api.Domain.Faculties;

[ApiController]
[Route(Routs.Faculties)]
public class FacultiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FacultiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<FacultyDto[]> GetFaculty(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetFacultyQuery(pageNumber, pageSize);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<Guid> CreateFaculty([FromBody] CreateFacultyRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateFacultyCommand(request.Name);
        var id = await _mediator.Send(command, cancellationToken);
        return id;
    }

    [HttpPost("add-departments")]
    public async Task CreateDepartments([FromBody] CreateFacultyDepartmentsRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateFacultyDepartmentsCommand(request.FacultyId, request.DepartmentId);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    public async Task DeleteFaculty([FromBody] RemoveFacultyRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveFacultyCommand(request.Id);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete("delete-departments")]
    public async Task DeleteDepartments([FromBody] RemoveFacultyDepartmentsRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveFacultyDepartmentsCommand(request.FacultyId);
        await _mediator.Send(command, cancellationToken);
    }
}