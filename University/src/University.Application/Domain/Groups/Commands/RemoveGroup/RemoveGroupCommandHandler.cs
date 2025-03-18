using MediatR;
using University.Core.Common;
using University.Core.Domain.Groups.Common;

namespace University.Application.Domain.Groups.Commands.RemoveGroup;

public class RemoveGroupCommandHandler : IRequestHandler<RemoveGroupCommand, Unit>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveGroupCommandHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
    {
        _groupRepository = groupRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveGroupCommand command, CancellationToken cancellationToken)
    {
        await _groupRepository.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}