using MediatR;

namespace University.Application.Domain.Teachers.Commands.CreateTeacher;

public record CreateTeacherCommand(string FirstName, string LastName, string MiddleName) : IRequest<Guid>;