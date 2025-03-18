using MediatR;

namespace University.Application.Domain.Teachers.Commands.UpdateTeacher;

public record UpdateTeacherCommand(Guid Id, string FirstName, string LastName, string MiddleName) : IRequest<Unit>;
