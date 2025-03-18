namespace University.Api.Domain.RecordBooks.Request;

public record CreateMarksRequest(Guid RecordBookId, Guid SubjectId, int Grade);