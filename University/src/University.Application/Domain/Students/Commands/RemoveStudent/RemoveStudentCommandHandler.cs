using MediatR;
using University.Core.Common;
using University.Core.Domain.Students.Common;

namespace University.Application.Domain.Students.Commands.RemoveStudent;

public class RemoveStudentCommandHandler : IRequestHandler<RemoveStudentCommand, Unit>
{
    private readonly IStudentsRepository _studentsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveStudentCommandHandler(IStudentsRepository studentsRepository, IUnitOfWork unitOfWork)
    {
        _studentsRepository = studentsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveStudentCommand command, CancellationToken cancellationToken)
    {
        await _studentsRepository.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}