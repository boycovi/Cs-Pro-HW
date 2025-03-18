using MediatR;
using University.Core.Common;
using University.Core.Domain.Faculties.Common;

namespace University.Application.Domain.Faculties.Commands.RemoveFaculty;

public class RemoveFacultyCommandHandler : IRequestHandler<RemoveFacultyCommand, Unit>
{
    private readonly IFacultyRepository _facultyRepository;

    private readonly IUnitOfWork _unitOfWork;

    public RemoveFacultyCommandHandler(IFacultyRepository facultyRepository, IUnitOfWork unitOfWork)
    {
        _facultyRepository = facultyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveFacultyCommand command, CancellationToken cancellationToken)
    {
        await _facultyRepository.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}