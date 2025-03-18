using MediatR;

namespace University.Application.Domain.Teachers.Commands.RemoveTeacherSubject;

public record RemoveTeacherSubjectCommand(Guid TeacherId) : IRequest<Unit>;