namespace University.Application.Domain.RecordBooks.Queries.GetRecordBook;

public record RecordBookDto
{
    public Guid Id { get; init; }

    public Guid StudentId { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string MiddleName { get; init; }

    public IReadOnlyCollection<MarkDto> Marks { get; init; }
}