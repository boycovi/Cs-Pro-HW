using MediatR;
using University.Core.Common;
using University.Core.Domain.Subjects.Common;
using University.Core.Domain.Subjects.Models;

namespace University.Application.Domain.Subjects.Commands.CreateSubject;

public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Guid>
{
    private readonly ISubjectsRepository _subjectsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubjectCommandHandler(ISubjectsRepository subjectsRepository, IUnitOfWork unitOfWork)
    {
        _subjectsRepository = subjectsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateSubjectCommand command, CancellationToken cancellationToken)
    {
        var subject = Subject.Create(command.Name, command.Code);
        await _subjectsRepository.AddAsync(subject);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return subject.Id;
    }
}