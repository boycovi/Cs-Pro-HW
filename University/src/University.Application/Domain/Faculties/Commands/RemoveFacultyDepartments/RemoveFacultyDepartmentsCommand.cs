using MediatR;

namespace University.Application.Domain.Faculties.Commands.RemoveFacultyDepartments;

public record RemoveFacultyDepartmentsCommand(Guid FacultyId) : IRequest<Unit>;
