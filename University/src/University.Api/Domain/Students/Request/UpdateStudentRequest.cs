namespace University.Api.Domain.Students.Request;

public record UpdateStudentRequest(Guid StudentId, string FirstName, string LastName, string MiddleName);