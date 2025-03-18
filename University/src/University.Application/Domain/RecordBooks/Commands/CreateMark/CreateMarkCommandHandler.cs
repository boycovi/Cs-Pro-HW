using MediatR;
using University.Core.Common;
using University.Core.Domain.RecordBooks.Common;
using University.Core.Domain.RecordBooks.Models;

namespace University.Application.Domain.RecordBooks.Commands.CreateMark;

public class CreateMarkCommandHandler : IRequestHandler<CreateMarkCommand, Guid>
{
    private readonly IMarkRepository _recordBookSubjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMarkCommandHandler(IMarkRepository recordBookSubjectRepository, IUnitOfWork unitOfWork)
    {
        _recordBookSubjectRepository = recordBookSubjectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateMarkCommand command, CancellationToken cancellationToken)
    {
        var record = RecordBookSubject.Create(command.RecordId, command.SubjectId, command.Grade);
        await _recordBookSubjectRepository.AddAsync(record);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return record.RecordId;
    }
}