using MediatR;
using University.Core.Common;
using University.Core.Domain.Groups.Common;
using University.Core.Domain.Groups.Models;

namespace University.Application.Domain.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGroupNameMustBeUniqueChecker _groupNameMustBeUniqueChecker;

    public CreateGroupCommandHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
    {
        _groupRepository = groupRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateGroupCommand command, CancellationToken cancellationToken)
    {
        var studentGroup = await Group.CreateAsync(command.Name, _groupNameMustBeUniqueChecker, cancellationToken);
        await _groupRepository.AddAsync(studentGroup);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return studentGroup.Id;
    }
}