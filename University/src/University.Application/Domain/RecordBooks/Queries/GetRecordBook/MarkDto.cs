namespace University.Application.Domain.RecordBooks.Queries.GetRecordBook;

public record MarkDto
{
    public Guid SubjectId { get; init; }

    public string SubjectName { get; init; }

    public int Grade { get; init; }
}