namespace University.Application.Domain.Students.Queries.GetStudent;

public record StudentDto
{
    public Guid Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string MiddleName { get; init; }

    public Guid GroupId { get; init; }

    public string GroupName { get; init; }
}