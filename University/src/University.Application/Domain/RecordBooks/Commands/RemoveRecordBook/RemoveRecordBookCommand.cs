using MediatR;

namespace University.Application.Domain.RecordBooks.Commands.RemoveRecordBook;

public record RemoveRecordBookCommand(Guid Id) : IRequest<Unit>;