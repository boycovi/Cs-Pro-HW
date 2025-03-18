namespace University.Api.Domain.Faculties.Request;

public record CreateFacultyDepartmentsRequest(Guid FacultyId, Guid DepartmentId);