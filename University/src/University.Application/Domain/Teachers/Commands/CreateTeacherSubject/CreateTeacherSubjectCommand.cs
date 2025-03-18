using MediatR;

namespace University.Application.Domain.Teachers.Commands.CreateTeacherSubject;

public record CreateTeacherSubjectCommand(Guid TeacherId, Guid SubjectId) : IRequest<Guid>;