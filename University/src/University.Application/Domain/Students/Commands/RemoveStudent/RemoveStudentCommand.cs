using MediatR;

namespace University.Application.Domain.Students.Commands.RemoveStudent;

public record RemoveStudentCommand(Guid Id) : IRequest<Unit>;