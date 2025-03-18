using MediatR;

namespace University.Application.Domain.Students.Commands.UpdateStudent;

public record UpdateStudentCommand(Guid Id, string FirstName, string LastName, string MiddleName) : IRequest<Unit>;