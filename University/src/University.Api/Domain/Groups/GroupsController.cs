using MediatR;
using Microsoft.AspNetCore.Mvc;
using University.Api.Common;
using University.Api.Domain.Groups.Request;
using University.Application.Domain.Groups.Commands.CreateGroup;
using University.Application.Domain.Groups.Commands.RemoveGroup;
using University.Application.Domain.Groups.Queries.GetGroup;

namespace University.Api.Domain.Groups;

[ApiController]
[Route(Routs.Groups)]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<GroupDto[]> GetGroup(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetGroupQuery(pageNumber, pageSize);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<Guid> CreateGroup([FromBody] CreateGroupsRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateGroupCommand(request.Name);
        var id = await _mediator.Send(command, cancellationToken);
        return id;
    }

    [HttpDelete]
    public async Task DeleteGroup([FromBody] RemoveGroupRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveGroupCommand(request.Id);
        await _mediator.Send(command, cancellationToken);
    }
}