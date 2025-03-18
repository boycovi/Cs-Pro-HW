namespace University.Api.Domain.Teachers.Request;

public record UpdateTeacherRequest(Guid TeacherId, string FirstName, string LastName, string MiddleName);