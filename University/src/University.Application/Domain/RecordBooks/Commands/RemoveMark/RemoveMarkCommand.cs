using MediatR;

namespace University.Application.Domain.RecordBooks.Commands.RemoveMark;

public record RemoveMarkCommand(Guid RecordId) : IRequest<Unit>;