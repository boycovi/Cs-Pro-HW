using MediatR;

namespace University.Application.Domain.Faculties.Commands.CreateFacultyDepartments;

public record CreateFacultyDepartmentsCommand(Guid FacultyId, Guid DepartmentId): IRequest<Guid>;