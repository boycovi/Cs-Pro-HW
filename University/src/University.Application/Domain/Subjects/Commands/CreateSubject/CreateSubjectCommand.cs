using MediatR;

namespace University.Application.Domain.Subjects.Commands.CreateSubject;

public record CreateSubjectCommand(string Name, int Code) : IRequest<Guid>;