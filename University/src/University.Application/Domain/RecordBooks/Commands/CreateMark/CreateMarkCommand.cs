using MediatR;

namespace University.Application.Domain.RecordBooks.Commands.CreateMark;

public record CreateMarkCommand(Guid RecordId, Guid SubjectId, int Grade) : IRequest<Guid>;