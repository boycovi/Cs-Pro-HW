using MediatR;

namespace University.Application.Domain.Students.Queries.GetStudent;

public record GetStudentsQuery(int PageNumber, int PageSize) : IRequest<StudentDto[]>;