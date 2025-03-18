using MediatR;
using Microsoft.EntityFrameworkCore;
using University.Application.Domain.Faculties.Queries.GetFaculty;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Domain.Faculties.Queries;

public class GetFacultyQueryHandler : IRequestHandler<GetFacultyQuery, FacultyDto[]>
{
    private readonly UniversityDbContext _universityDbContext;

    public GetFacultyQueryHandler(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task<FacultyDto[]> Handle(GetFacultyQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _universityDbContext.Faculties.AsNoTracking()
            .Include(x => x.Departments);

        var skip = (request.PageNumber - 1) * request.PageSize;
        var data = await sqlQuery
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new FacultyDto()
            {
                Id = x.Id,
                Name = x.Name,
                DepartmentsCollection = x.Departments.Select(department => new DepartmentsDto()
                {
                    DepartmentId = department.DepartmenttId,
                    DepartmentName = department.Department.Name
                }).ToList()
            }).ToArrayAsync(cancellationToken);
        return data;
    }
}