using MediatR;
using University.Core.Common;
using University.Core.Domain.RecordBooks.Common;

namespace University.Application.Domain.RecordBooks.Commands.RemoveRecordBook;

public class RemoveRecordBookCommandHandler : IRequestHandler<RemoveRecordBookCommand, Unit>
{
    private readonly IRecordBookRepository _recordBookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveRecordBookCommandHandler(IRecordBookRepository recordBookRepository, IUnitOfWork unitOfWork)
    {
        _recordBookRepository = recordBookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveRecordBookCommand command, CancellationToken cancellationToken)
    {
        await _recordBookRepository.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}