using MediatR;

namespace University.Application.Domain.RecordBooks.Commands.CreateRecordBook;

public record CreateRecordBookCommand(Guid StudentId) : IRequest<Guid>;