using MediatR;
using University.Core.Common;
using University.Core.Domain.Teachers.Common;

namespace University.Application.Domain.Teachers.Commands.RemoveTeacher;

public class RemoveTeacherCommandHandler : IRequestHandler<RemoveTeacherCommand, Unit>
{
    private readonly ITeachersRepository _teachersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveTeacherCommandHandler(ITeachersRepository teachersRepository, IUnitOfWork unitOfWork)
    {
        _teachersRepository = teachersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveTeacherCommand command, CancellationToken cancellationToken)
    {
        await _teachersRepository.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}