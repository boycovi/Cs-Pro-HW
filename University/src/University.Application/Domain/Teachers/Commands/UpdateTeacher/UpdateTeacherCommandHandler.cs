using MediatR;
using University.Core.Common;
using University.Core.Domain.Teachers.Common;
using University.Core.Domain.Teachers.Data;

namespace University.Application.Domain.Teachers.Commands.UpdateTeacher;

public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, Unit>
{
    private readonly ITeachersRepository _teachersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITeacherFirstNameMustBeInRangeChecker _teacherFirstNameMustBeInRangeChecker;

    public UpdateTeacherCommandHandler(ITeachersRepository teachersRepository, IUnitOfWork unitOfWork, ITeacherFirstNameMustBeInRangeChecker teacherFirstNameMustBeInRangeChecker)
    {
        _teachersRepository = teachersRepository;
        _unitOfWork = unitOfWork;
        _teacherFirstNameMustBeInRangeChecker = teacherFirstNameMustBeInRangeChecker;
    }

    public async Task<Unit> Handle(UpdateTeacherCommand command, CancellationToken cancellationToken)
    {
        var original = await _teachersRepository.FindAsync(command.Id);
        var data = new UpdateTeacherData(command.FirstName, command.LastName, command.MiddleName);
        await original.UpdateAsync(data, _teacherFirstNameMustBeInRangeChecker, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}