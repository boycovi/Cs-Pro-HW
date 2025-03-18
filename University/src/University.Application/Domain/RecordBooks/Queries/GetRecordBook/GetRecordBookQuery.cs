using MediatR;

namespace University.Application.Domain.RecordBooks.Queries.GetRecordBook;

public record GetRecordBookQuery(int PageNumber, int PageSize) : IRequest<RecordBookDto[]>;