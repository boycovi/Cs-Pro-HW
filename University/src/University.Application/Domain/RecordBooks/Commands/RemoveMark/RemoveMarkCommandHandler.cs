using MediatR;
using University.Core.Common;
using University.Core.Domain.RecordBooks.Common;

namespace University.Application.Domain.RecordBooks.Commands.RemoveMark;

public class RemoveMarkCommandHandler : IRequestHandler<RemoveMarkCommand, Unit>
{
    private readonly IMarkRepository _markRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveMarkCommandHandler(IMarkRepository markRepository, IUnitOfWork unitOfWork)
    {
        _markRepository = markRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveMarkCommand command, CancellationToken cancellationToken)
    {
        await _markRepository.DeleteAsync(command.RecordId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}