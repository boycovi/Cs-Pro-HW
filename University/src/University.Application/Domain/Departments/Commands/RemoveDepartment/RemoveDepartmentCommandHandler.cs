using MediatR;
using University.Core.Common;
using University.Core.Domain.Departments.Common;

namespace University.Application.Domain.Departments.Commands.RemoveDepartment;

public class RemoveDepartmentCommandHandler : IRequestHandler<RemoveDepartmentCommand, Unit>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveDepartmentCommand command, CancellationToken cancellationToken)
    {
        await _departmentRepository.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}