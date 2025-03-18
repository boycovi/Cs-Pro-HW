using MediatR;
using University.Core.Common;
using University.Core.Domain.Faculties.Common;

namespace University.Application.Domain.Faculties.Commands.RemoveFacultyDepartments;

public class RemoveFacultyDepartmentsCommandHandler : IRequestHandler<RemoveFacultyDepartmentsCommand, Unit>
{
    private readonly IFacultyDepartmentRepository _facultyDepartmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveFacultyDepartmentsCommandHandler(IFacultyDepartmentRepository facultyDepartmentRepository, IUnitOfWork unitOfWork)
    {
        _facultyDepartmentRepository = facultyDepartmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveFacultyDepartmentsCommand command, CancellationToken cancellationToken)
    {
        await _facultyDepartmentRepository.DeleteAsync(command.FacultyId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}