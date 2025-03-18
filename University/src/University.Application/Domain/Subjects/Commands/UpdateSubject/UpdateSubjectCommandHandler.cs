using MediatR;
using University.Core.Common;
using University.Core.Domain.Subjects.Common;
using University.Core.Domain.Subjects.Data;

namespace University.Application.Domain.Subjects.Commands.UpdateSubject;

public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, Unit>
{
    private readonly ISubjectNameMustBeUniqueChecker _subjectNameMustBeUnique;
    private readonly ISubjectsRepository _subjectsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSubjectCommandHandler(ISubjectsRepository subjectsRepository, IUnitOfWork unitOfWork, ISubjectNameMustBeUniqueChecker subjectNameMustBeUnique)
    {
        _subjectsRepository = subjectsRepository;
        _unitOfWork = unitOfWork;
        _subjectNameMustBeUnique = subjectNameMustBeUnique;
    }

    public async Task<Unit> Handle(UpdateSubjectCommand command, CancellationToken cancellationToken)
    {
        var original = await _subjectsRepository.FindAsync(command.Id);
        var data = new UpdateSubjectData(command.Name, command.Code);
        await original.UpdateAsync(data, _subjectNameMustBeUnique, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}