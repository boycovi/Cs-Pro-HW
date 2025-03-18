using MediatR;
using University.Core.Common;
using University.Core.Domain.Faculties.Common;
using University.Core.Domain.Faculties.Models;

namespace University.Application.Domain.Faculties.Commands.CreateFaculty;

public class CreateFacultyCommandHandler : IRequestHandler<CreateFacultyCommand, Guid>
{
    private readonly IFacultyRepository _facultyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFacultyNameMustBeUniqueChecker _facultyNameMustBeUniqueChecker;

    public CreateFacultyCommandHandler(IFacultyRepository facultyRepository, IUnitOfWork unitOfWork)
    {
        _facultyRepository = facultyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateFacultyCommand command, CancellationToken cancellationToken)
    {
        var faculty = await Faculty.CreateAsync(command.Name, _facultyNameMustBeUniqueChecker, cancellationToken);
        await _facultyRepository.AddAsync(faculty);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return faculty.Id;
    }
}