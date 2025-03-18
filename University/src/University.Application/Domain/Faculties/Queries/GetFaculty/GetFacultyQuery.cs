using MediatR;

namespace University.Application.Domain.Faculties.Queries.GetFaculty;

public record GetFacultyQuery(int PageNumber, int PageSize) : IRequest<FacultyDto[]>;