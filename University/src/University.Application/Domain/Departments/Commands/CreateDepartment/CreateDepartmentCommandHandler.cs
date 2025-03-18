using MediatR;
using University.Core.Common;
using University.Core.Domain.Departments.Common;
using University.Core.Domain.Departments.Models;

namespace University.Application.Domain.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDepartmentNameMustBeUniqueChecker _departmentNameMustBeUniqueChecker;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork, IDepartmentNameMustBeUniqueChecker departmentNameMustBeUniqueChecker)
    {
        _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
        _departmentNameMustBeUniqueChecker = departmentNameMustBeUniqueChecker;
    }

    public async Task<Guid> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        var department = await Department.CreateAsync(command.Name, _departmentNameMustBeUniqueChecker, cancellationToken);
        await _departmentRepository.AddAsync(department);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return department.Id;
    }
}