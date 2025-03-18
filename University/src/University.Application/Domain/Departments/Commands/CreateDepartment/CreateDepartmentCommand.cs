using MediatR;

namespace University.Application.Domain.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand(string Name) : IRequest<Guid>;