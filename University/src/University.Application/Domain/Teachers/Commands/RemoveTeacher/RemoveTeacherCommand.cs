using MediatR;

namespace University.Application.Domain.Teachers.Commands.RemoveTeacher;

public record RemoveTeacherCommand(Guid Id) : IRequest<Unit>;