using MediatR;

namespace University.Application.Domain.Groups.Commands.RemoveGroup
{
    public record RemoveGroupCommand(Guid Id) : IRequest<Unit>;
}
