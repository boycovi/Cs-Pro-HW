using MediatR;
using University.Core.Common;
using University.Core.Domain.Teachers.Common;

namespace University.Application.Domain.Teachers.Commands.RemoveTeacherSubject;

public class RemoveTeacherSubjectCommandHandler : IRequestHandler<RemoveTeacherSubjectCommand, Unit>
{
    private readonly ITeacherSubjectsRepository _teacherSubjectsRepository;

    private readonly IUnitOfWork _unitOfWork;

    public RemoveTeacherSubjectCommandHandler(ITeacherSubjectsRepository teacherSubjectsRepository, IUnitOfWork unitOfWork)
    {
        _teacherSubjectsRepository = teacherSubjectsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveTeacherSubjectCommand command, CancellationToken cancellationToken)
    {
        await _teacherSubjectsRepository.DeleteAsync(command.TeacherId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}