namespace University.Api.Domain.Students.Request;

public record CreateStudentRequest(string FirstName, string LastName, string MiddleName, Guid GroupId);