using MediatR;
using University.Core.Common;
using University.Core.Domain.Students.Common;
using University.Core.Domain.Students.Data;

namespace University.Application.Domain.Students.Commands.UpdateStudent;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Unit>
{
    private readonly IStudentsRepository _studentsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStudentFirstNameMustBeInRangeChecker _studentNameMustBeInRangeChecker;

    public UpdateStudentCommandHandler(
        IStudentsRepository studentsRepository, 
        IUnitOfWork unitOfWork, 
        IStudentFirstNameMustBeInRangeChecker studentNameMustBeInRangeChecker)
    {
        _studentsRepository = studentsRepository;
        _unitOfWork = unitOfWork;
        _studentNameMustBeInRangeChecker = studentNameMustBeInRangeChecker;
    }

    public async Task<Unit> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
    {
        var original = await _studentsRepository.FindAsync(command.Id);
        var data = new UpdateStudentData(command.FirstName, command.LastName, command.MiddleName);
        await original.UpdateAsync(data, _studentNameMustBeInRangeChecker, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}