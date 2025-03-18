namespace University.Application.Domain.Faculties.Queries.GetFaculty;

public record DepartmentsDto
{
   public Guid DepartmentId { get; init; }

   public string DepartmentName { get; init;}
}