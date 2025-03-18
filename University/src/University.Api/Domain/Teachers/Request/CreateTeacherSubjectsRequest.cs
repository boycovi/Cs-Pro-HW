namespace University.Api.Domain.Teachers.Request;

public record CreateTeacherSubjectsRequest(Guid TeacherId, Guid SubjectId);