using MediatR;
using Microsoft.AspNetCore.Mvc;
using University.Api.Common;
using University.Api.Domain.Departments.Request;
using University.Application.Domain.Departments.Commands.CreateDepartment;
using University.Application.Domain.Departments.Commands.RemoveDepartment;
using University.Application.Domain.Departments.Queries.GetDepartment;

namespace University.Api.Domain.Departments;

[ApiController]
[Route(Routs.Departments)]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<DepartmentDto[]> GetDepartment(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetDepartmentQuery(pageNumber, pageSize);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<Guid> CreateDepartment([FromBody] CreateDepartmentsRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateDepartmentCommand(request.Name);
        var id = await _mediator.Send(command, cancellationToken);
        return id;
    }

    [HttpDelete]
    public async Task DeleteDepartment([FromBody] RemoveDepartmentRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveDepartmentCommand(request.Id);
        await _mediator.Send(command, cancellationToken);
    }
}