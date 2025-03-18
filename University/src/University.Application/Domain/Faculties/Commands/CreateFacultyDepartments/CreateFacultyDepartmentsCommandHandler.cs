using MediatR;
using University.Core.Common;
using University.Core.Domain.Faculties.Common;
using University.Core.Domain.Faculties.Models;

namespace University.Application.Domain.Faculties.Commands.CreateFacultyDepartments;

public class CreateFacultyDepartmentsCommandHandler : IRequestHandler<CreateFacultyDepartmentsCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IFacultyDepartmentRepository _facultyDepartmentRepository;

    public CreateFacultyDepartmentsCommandHandler(IUnitOfWork unitOfWork, IFacultyDepartmentRepository facultyDepartmentRepository)
    {
        _unitOfWork = unitOfWork;
        _facultyDepartmentRepository = facultyDepartmentRepository;
    }

    public async Task<Guid> Handle(CreateFacultyDepartmentsCommand command, CancellationToken cancellationToken)
    {
        var facultyDepartments = FacultyDepartment.Create(command.FacultyId, command.DepartmentId);
        await _facultyDepartmentRepository.AddAsync(facultyDepartments);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return facultyDepartments.FacultyId;
    }
}