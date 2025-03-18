using MediatR;

namespace University.Application.Domain.Subjects.Commands.RemoveSubject;

public record RemoveSubjectCommand(Guid Id) : IRequest<Unit>;