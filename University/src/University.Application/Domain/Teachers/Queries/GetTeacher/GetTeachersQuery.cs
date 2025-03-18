using MediatR;

namespace University.Application.Domain.Teachers.Queries.GetTeacher;

public record GetTeachersQuery(int PageNumber, int PageSize) : IRequest<TeacherDto[]>;