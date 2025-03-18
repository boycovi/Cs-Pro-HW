using MediatR;
using Microsoft.EntityFrameworkCore;
using University.Application.Domain.Departments.Queries.GetDepartment;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Departments.Queries;

public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, DepartmentDto[]>
{
    private readonly UniversityDbContext _universityDbContext;

    public GetDepartmentQueryHandler(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<DepartmentDto[]> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _universityDbContext.Departments.AsNoTracking();
        var skip = (request.PageNumber - 1) * request.PageSize;
        var data = await sqlQuery
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new DepartmentDto()
            {
                Id = x.Id,
                Name = x.Name
            }).ToArrayAsync(cancellationToken);

        return data;
    }
}