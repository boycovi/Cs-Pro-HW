namespace University.Application.Domain.Teachers.Queries.GetTeacher;

public record SubjectsDto
{
    public Guid SubjectsId { get; init; }

    public string SubjectName { get; init; }

    public int SubjectCode { get; init; }
}