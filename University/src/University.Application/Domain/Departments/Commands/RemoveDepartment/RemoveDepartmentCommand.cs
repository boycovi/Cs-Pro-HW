using MediatR;

namespace University.Application.Domain.Departments.Commands.RemoveDepartment;

public record RemoveDepartmentCommand(Guid Id) : IRequest<Unit>;