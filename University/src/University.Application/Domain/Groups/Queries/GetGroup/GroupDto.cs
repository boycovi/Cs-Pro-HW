namespace University.Application.Domain.Groups.Queries.GetGroup;

public record GroupDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public IReadOnlyCollection<StudentsDto> StudentsCollection { get; init; }
}