namespace University.Application.Domain.Groups.Queries.GetGroup;

public record StudentsDto
{
    public Guid StudentId { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string MiddleName { get; init; }
}