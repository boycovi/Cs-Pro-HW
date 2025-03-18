using MediatR;
using University.Core.Common;
using University.Core.Domain.Subjects.Common;

namespace University.Application.Domain.Subjects.Commands.RemoveSubject;

public class RemoveSubjectCommandHandler : IRequestHandler<RemoveSubjectCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubjectsRepository _subjectsRepository;

    public RemoveSubjectCommandHandler(IUnitOfWork unitOfWork, ISubjectsRepository subjectsRepository)
    {
        _unitOfWork = unitOfWork;
        _subjectsRepository = subjectsRepository;
    }

    public async Task<Unit> Handle(RemoveSubjectCommand command, CancellationToken cancellationToken)
    {
        await _subjectsRepository.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}   
