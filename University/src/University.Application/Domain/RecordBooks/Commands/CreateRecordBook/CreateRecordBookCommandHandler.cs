using MediatR;
using University.Core.Common;
using University.Core.Domain.RecordBooks.Common;
using University.Core.Domain.RecordBooks.Models;

namespace University.Application.Domain.RecordBooks.Commands.CreateRecordBook;

public class CreateRecordBookCommandHandler : IRequestHandler<CreateRecordBookCommand, Guid>
{
    private readonly IRecordBookRepository _recordBookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStudentIdMustBeUniqueChecker _studentIdMustBeUniqueChecker;

    public CreateRecordBookCommandHandler(IRecordBookRepository recordBookRepository, IUnitOfWork unitOfWork, IStudentIdMustBeUniqueChecker studentIdMustBeUniqueChecker)
    {
        _recordBookRepository = recordBookRepository;
        _unitOfWork = unitOfWork;
        _studentIdMustBeUniqueChecker = studentIdMustBeUniqueChecker;
    }

    public async Task<Guid> Handle(CreateRecordBookCommand command, CancellationToken cancellationToken)
    {
        var recordBook = await RecordBook.CreateAsync(command.StudentId, _studentIdMustBeUniqueChecker);
        await _recordBookRepository.AddAsync(recordBook);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return recordBook.Id;
    }
}