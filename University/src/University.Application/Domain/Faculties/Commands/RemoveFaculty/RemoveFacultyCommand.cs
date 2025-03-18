using MediatR;

namespace University.Application.Domain.Faculties.Commands.RemoveFaculty;

public record RemoveFacultyCommand(Guid Id) : IRequest<Unit>;
