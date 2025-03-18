using MediatR;

namespace University.Application.Domain.Subjects.Commands.UpdateSubject;

public record UpdateSubjectCommand(Guid Id, string Name, int Code) : IRequest<Unit>;