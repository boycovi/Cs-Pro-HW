using MediatR;

namespace University.Application.Domain.Subjects.Queries.GetSubjects;

public record GetSubjectsQuery(int PageNumber, int PageSize) : IRequest<SubjectDto[]>;