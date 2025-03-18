namespace University.Application.Domain.Faculties.Queries.GetFaculty;

public class FacultyDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public IReadOnlyCollection<DepartmentsDto> DepartmentsCollection { get; init; }
}