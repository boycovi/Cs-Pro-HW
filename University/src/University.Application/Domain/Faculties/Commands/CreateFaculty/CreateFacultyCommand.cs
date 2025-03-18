using MediatR;

namespace University.Application.Domain.Faculties.Commands.CreateFaculty;

public record CreateFacultyCommand(string Name) : IRequest<Guid>;