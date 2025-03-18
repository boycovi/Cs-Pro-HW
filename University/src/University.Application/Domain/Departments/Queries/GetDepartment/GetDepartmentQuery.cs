using MediatR;

namespace University.Application.Domain.Departments.Queries.GetDepartment;

public record GetDepartmentQuery(int PageNumber, int PageSize) : IRequest<DepartmentDto[]>;
