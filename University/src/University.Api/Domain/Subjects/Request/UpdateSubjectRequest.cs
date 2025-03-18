namespace University.Api.Domain.Subjects.Request;

public record UpdateSubjectRequest(Guid Id, string Name, int Code);
